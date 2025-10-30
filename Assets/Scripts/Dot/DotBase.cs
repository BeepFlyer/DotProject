using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Random = UnityEngine.Random;

public class DotBase : MonoBehaviour
{
    protected ulong id;
    protected Rigidbody rb;

    
    public bool sendFlag = true;// 多个物体碰撞时，让一个物体进行报告，关闭其他物体的碰撞Flag

    protected float randomSeed = 0;

    protected virtual void Awake()
    {
        Assert.IsTrue(gameObject!=null,"未找到游戏对象");
        rb = gameObject.GetComponent<Rigidbody>();

    }
    

    protected void OnEnable()
    {
        randomSeed = Random.Range(0, 360);
        rb.velocity = Vector3.zero;
        sendFlag = true;
    }

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
        randomSeed = Random.Range(0, 360);
        return Vector3.zero;
    }
}
