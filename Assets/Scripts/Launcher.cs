using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using UnityEditor.Compilation;


public class Launcher : MonoBehaviour
{
    private void Awake()
    {
        God.Init();
    }
    
    [Button("立即编译")]
    public void ForceCompile()
    {
        // 开始编译所有脚本
        CompilationPipeline.RequestScriptCompilation();
        Debug.Log("已请求 Unity 编译脚本");
    }

    
    
}
