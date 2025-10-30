using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MusicManager
{
    private AudioSource source;
    public void Init()
    {
        GameObject sourceObj = (GameObject)Resources.Load("prefab/backGroundMusic");
        GameObject sourceObj_ins =  GameObject.Instantiate(sourceObj, Vector3.zero, Quaternion.identity);
        source = sourceObj_ins.GetOrAddComponent<AudioSource>();
    }
}
