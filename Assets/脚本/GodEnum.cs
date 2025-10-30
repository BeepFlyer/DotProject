using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PrefabEnum
{
    BlackDot = 0,
}

public enum DotType
{
    BlackDot,
}

public static class GlobalMapping
{
    public static Dictionary<PrefabEnum, DotType> prefabToDotType = new Dictionary<PrefabEnum, DotType>()
    {
        [PrefabEnum.BlackDot] = DotType.BlackDot,
    };
    
    public static Dictionary<DotType,PrefabEnum> dotType2prefab = new Dictionary<DotType,PrefabEnum>()
    {
        [DotType.BlackDot] = PrefabEnum.BlackDot,
    };

}



