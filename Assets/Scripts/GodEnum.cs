using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PrefabEnum
{
    BlackDot = 0,
    Player = 1,
    WhiteDot = 2,
    EnergyText = 3,
    EnergyCanvas = 4,

}

public enum DotType
{
    BlackDot,
    MainDot,
    WhiteDot,
}

public enum Music
{
    StartJourney,UnlockNewPath
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

    public static Dictionary<Music, string> MusicPath = new Dictionary<Music, string>()
    {
        [Music.StartJourney] = "Assets/Resources/sound/bgm/startJourney.wav",
        [Music.UnlockNewPath] = "Assets/Resources/sound/bgm/unlockNewPath.wav",
    };


}



