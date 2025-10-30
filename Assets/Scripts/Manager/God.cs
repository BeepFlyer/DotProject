using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//所有管理器都在这
public static class God
{
    public static DotManager dotManager;

    public static void Init()
    {
        Debug.Log("God初始化中");
        dotManager = new DotManager();
        Debug.Log("God初始化完成");
    }
}
