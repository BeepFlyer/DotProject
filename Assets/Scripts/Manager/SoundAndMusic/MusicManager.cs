using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.IO;
using UnityEngine.Assertions;


public class MusicManager
{
    private AudioSource source;
    private Dictionary<Music, AudioClip> musicDic;
    public void Init()
    {
        GameObject sourceObj = (GameObject)Resources.Load("Prefab/Other/backGroundMusic");
        GameObject sourceObj_ins =  GameObject.Instantiate(sourceObj, Vector3.zero, Quaternion.identity);
        source = sourceObj_ins.GetOrAddComponent<AudioSource>();
        source.volume = 0.4f;
        musicDic = new Dictionary<Music, AudioClip>();
    }

    public void Play(Music music)
    {
        source.clip = GetMusic(music);
        source.Play();
    }

    AudioClip GetMusic(Music i)
    {
        if (musicDic.ContainsKey(i))
        {
            return musicDic[i];
        }
        else
        {
            byte[] wavBytes = File.ReadAllBytes(GlobalMapping.MusicPath[i]);
            AudioClip clip = WavUtility.ToAudioClip(wavBytes,Convert.ToString(i));
            Assert.IsTrue(clip!=null,$"<color=red>错误创建Audio{Convert.ToString(i)}</color>");
            musicDic[i] = clip;
            return musicDic[i];
        }
    }
}
