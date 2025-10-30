using System;
using System.IO;
using UnityEngine;

public static class WavUtility
{
    /// <summary>
    /// 从内存中的 WAV 二进制创建 AudioClip（支持 PCM 8/16/24/32 和 IEEE Float 32，以及多声道）。
    /// 对于压缩的 WAV（非 PCM/IEEE float），会返回 null。
    /// </summary>
    public static AudioClip ToAudioClip(byte[] wavBytes, string clipName = "FromMemory")
    {
        if (wavBytes == null || wavBytes.Length < 44)
            throw new ArgumentException("wavBytes 太短或为空");

        using (var ms = new MemoryStream(wavBytes))
        using (var br = new BinaryReader(ms))
        {
            // ---- RIFF header ----
            var riff = new string(br.ReadChars(4));
            if (riff != "RIFF")
                throw new Exception("不是有效的 RIFF 文件");

            int riffChunkSize = br.ReadInt32(); // 文件大小 - 8
            var wave = new string(br.ReadChars(4));
            if (wave != "WAVE")
                throw new Exception("不是 WAVE 文件");

            // ---- 找到 fmt chunk ----
            int audioFormat = -1;
            int numChannels = 0;
            int sampleRate = 0;
            int byteRate = 0;
            int blockAlign = 0;
            int bitsPerSample = 0;
            bool fmtFound = false;
            bool dataFound = false;
            long dataChunkPos = 0;
            int dataChunkSize = 0;

            // 循环读取 chunk，直到找到 fmt 和 data
            while (ms.Position < ms.Length)
            {
                // 避免读取超出
                if (ms.Length - ms.Position < 8)
                    break;

                string chunkId = new string(br.ReadChars(4));
                int chunkSize = br.ReadInt32();

                // 处理 fmt chunk（可能不是第一个）
                if (chunkId == "fmt ")
                {
                    fmtFound = true;
                    long fmtChunkStart = ms.Position;

                    audioFormat = br.ReadInt16();         // 1 = PCM, 3 = IEEE float
                    numChannels = br.ReadInt16();
                    sampleRate = br.ReadInt32();
                    byteRate = br.ReadInt32();
                    blockAlign = br.ReadInt16();
                    bitsPerSample = br.ReadInt16();

                    // skip any extra fmt bytes
                    int fmtExtra = chunkSize - (int)(ms.Position - fmtChunkStart);
                    if (fmtExtra > 0)
                        br.ReadBytes(fmtExtra);
                }
                else if (chunkId == "data")
                {
                    dataFound = true;
                    dataChunkPos = ms.Position;
                    dataChunkSize = chunkSize;
                    // move position to end of data chunk (we'll re-read later)
                    br.BaseStream.Seek(chunkSize, SeekOrigin.Current);
                }
                else
                {
                    // 跳过其它 chunk（如 LIST, fact 等）
                    br.ReadBytes(chunkSize);
                }

                // chunkSize 可能为奇数 -> chunks are word (2-byte) aligned; advance 1 byte if needed
                if (chunkSize % 2 == 1)
                    ms.Position += 1;
            }

            if (!fmtFound)
                throw new Exception("fmt chunk 未找到");
            if (!dataFound)
                throw new Exception("data chunk 未找到");

            // 支持检查：音频格式
            if (audioFormat != 1 && audioFormat != 3)
                throw new Exception($"不支持的 WAV 音频格式: {audioFormat} (only PCM(1) and IEEE_FLOAT(3) supported)");

            // 计算样本数（总样本点，不是帧）
            int bytesPerSample = bitsPerSample / 8;
            if (bytesPerSample <= 0)
                throw new Exception("bitsPerSample 无效: " + bitsPerSample);

            long totalSampleCount = dataChunkSize / bytesPerSample;
            int totalFrameCount = (int)(totalSampleCount / numChannels); // 每声道的采样点数

            // 读取 data chunk 的真实数据
            br.BaseStream.Seek(dataChunkPos, SeekOrigin.Begin);
            byte[] rawData = br.ReadBytes(dataChunkSize);

            // 转换为 float[]（Unity 用的线性 PCM float，范围 -1..1），保持交错（interleaved）格式
            float[] floatData = new float[totalSampleCount];
            int idx = 0;

            if (audioFormat == 1) // PCM
            {
                if (bitsPerSample == 8)
                {
                    // 8-bit PCM is unsigned 0..255, center 128
                    for (int i = 0; i < rawData.Length; i++)
                    {
                        floatData[idx++] = (rawData[i] - 128f) / 128f;
                    }
                }
                else if (bitsPerSample == 16)
                {
                    for (int i = 0; i < rawData.Length; i += 2)
                    {
                        short s = (short)(rawData[i] | (rawData[i + 1] << 8));
                        floatData[idx++] = s / 32768f;
                    }
                }
                else if (bitsPerSample == 24)
                {
                    for (int i = 0; i < rawData.Length; i += 3)
                    {
                        int value = (rawData[i] | (rawData[i + 1] << 8) | (rawData[i + 2] << 16));
                        // sign extend
                        if ((value & 0x800000) != 0)
                            value |= unchecked((int)0xFF000000);
                        floatData[idx++] = value / 8388608f; // 2^23
                    }
                }
                else if (bitsPerSample == 32)
                {
                    for (int i = 0; i < rawData.Length; i += 4)
                    {
                        int value = BitConverter.ToInt32(rawData, i);
                        floatData[idx++] = value / 2147483648f; // 2^31
                    }
                }
                else
                {
                    throw new Exception("不支持的 PCM 位深: " + bitsPerSample);
                }
            }
            else // IEEE float (audioFormat == 3), 常见为 32-bit float
            {
                if (bitsPerSample != 32)
                    throw new Exception("IEEE float 但 bitsPerSample 不是 32");

                for (int i = 0; i < rawData.Length; i += 4)
                {
                    float f = BitConverter.ToSingle(rawData, i);
                    floatData[idx++] = f;
                }
            }

            // Unity AudioClip 需要 float[] 长度为 frames * channels，且数据是交错的（samples already interleaved）
            AudioClip clip = AudioClip.Create(clipName, totalFrameCount, numChannels, sampleRate, false);
            // AudioClip.SetData 接受的 samples 长度 = frames * channels
            // 但为了安全，SetData 的 samples 数目不能超过 clip.samples * clip.channels
            try
            {
                clip.SetData(floatData, 0);
            }
            catch (Exception e)
            {
                // 兼容性检查：如果样本数量不匹配，尝试截断或填充零
                int expected = clip.samples * clip.channels;
                float[] adjusted = new float[expected];
                int copyLen = Math.Min(expected, floatData.Length);
                Array.Copy(floatData, adjusted, copyLen);
                if (copyLen < expected)
                    for (int i = copyLen; i < expected; i++) adjusted[i] = 0f;
                clip.SetData(adjusted, 0);
            }

            return clip;
        }
    }
}
