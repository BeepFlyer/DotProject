using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//所有管理器都在这
public static class God
{
    public static DotManager dotManager;
    public static TimeManager timeManager;
    public static MusicManager musicManager;

    public static void Init()
    {
        Debug.Log("God初始化中");
        
        InitTimer();
        
        dotManager = new DotManager();

        musicManager = new MusicManager();
        musicManager.Init();
        
        Debug.Log("God初始化完成");
    }


    static void InitTimer()
    {
        GameObject go= GameObject.Find("Launcher");
        timeManager = go.GetComponent<TimeManager>();
    }
}
