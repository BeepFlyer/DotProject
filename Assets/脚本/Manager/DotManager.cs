using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotManager
{
    private DotFactory factory;
    private GameObject dotHolder;

    private Dictionary<ulong,DotBase> dotList = new Dictionary<ulong, DotBase>(500);

    public DotManager()
    {
        Debug.Log("DotManager初始化中");
        factory = new DotFactory();
        factory.SetDotManager(this);
        dotHolder = GameObject.Find("/dotsParent");
        factory.SetDotHolder(dotHolder);
        Debug.Log("DotManager初始化完成");

    }
    
    public DotBase Spawn(string typeName,Vector3 pos)
    {
        DotBase script = factory.Spawn(typeName, pos);
        if (script != null)
        {
            dotList[script.GetId()] = script;
        }

        return script;
    }

}
