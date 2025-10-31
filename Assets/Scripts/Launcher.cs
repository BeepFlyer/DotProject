using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using UnityEditor.Compilation;

[DefaultExecutionOrder(0)]
public class Launcher : MonoBehaviour
{
    private void Awake()
    {
        God.Init();
    }

    private void Start()
    {
        God.LateInit();
    }

    [Button("立即编译")]
    public void ForceCompile()
    {
        // 开始编译所有脚本
        CompilationPipeline.RequestScriptCompilation();
        Debug.Log("已请求 Unity 编译脚本");
    }

    
    
}
