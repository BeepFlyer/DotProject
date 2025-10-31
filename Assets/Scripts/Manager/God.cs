using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions;

//所有管理器都在这
public static class God
{
    public static DotManager dotManager;
    public static TimeManager timeManager;
    public static MusicManager musicManager;
    public static PlayerMain player;
    public static MapConfig mapConfig;
    public static DevelopManager dev;
    public static EnergyCanvasMgr energyCanvasMgr;

    public static void Init()
    {
        Debug.Log("God初始化中");
        
        InitTimer();
        
        dotManager = new DotManager();

        musicManager = new MusicManager();
        musicManager.Init();
        musicManager.Play(Music.UnlockNewPath);
        
        mapConfig = GameObject.Find("MapConfig").GetComponent<MapConfig>();

        dev = new DevelopManager();

        energyCanvasMgr = new EnergyCanvasMgr();
        energyCanvasMgr.Init();


        
        Debug.Log("God初始化完成");
    }

    public static void LateInit()
    {
        InitPlayer();

    }


    static void InitTimer()
    {
        GameObject go= GameObject.Find("Launcher");
        timeManager = go.GetComponent<TimeManager>();
    }

    static void InitPlayer()
    {
        GameObject parent = GameObject.Find("PlayerHolder");
        Assert.IsNotNull(parent,"未找到玩家持有者");
        Debug.Log($"找到玩家持有者{parent.name}");
        GameObject playerObj = ObjectPool.Instance.GetObject((int) PrefabEnum.Player, parent.transform.position,
            parent.transform.rotation);
        Assert.IsNotNull(playerObj,"生成玩家失败");
        Debug.Log($"生成玩家{playerObj.name}");
        playerObj.transform.SetParent(parent.transform);
        player = playerObj.GetOrAddComponent<PlayerMain>();
        player.Init();
        dotManager.InitPlayer();

    }
}
