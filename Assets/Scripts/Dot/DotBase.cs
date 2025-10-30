using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotBase : MonoBehaviour
{
    protected ulong id;
    
    public bool sendFlag = true;// 多个物体碰撞时，让一个物体进行报告，关闭其他物体的碰撞Flag


    public DotType Type
    {
        get
        {
            return _type;
        }
        set
        {
            _type = value;
        }
    }
    protected DotType _type;
    
    public void SetId(ulong id)
    {
        this.id = id;
    }
    
    public ulong GetId()
    {
        return this.id;
    }

    public virtual void OnDotPressed()
    {
        Debug.Log(gameObject.name+"被按压了");
    }

    public virtual Vector3 GetSpeed()
    {
        return Vector3.zero;
    }
}
