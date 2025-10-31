using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Random = UnityEngine.Random;

public class DotBase : MonoBehaviour,IHasEnergy
{
    #region 属性
    protected ulong id;
    protected Rigidbody rb;
    protected float originScale = 1;

    public float Energy
    {
        get { return _energy; }
        set { _energy = value; }
    }
    private float _energy = 0;
    
    public float MaxEnergy
    {
        get { return _maxEnergy; }
        set { _maxEnergy = value; }
    }
    private float _maxEnergy = 100;

    
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


    
    public bool sendFlag = true;// 多个物体碰撞时，让一个物体进行报告，关闭其他物体的碰撞Flag

    protected float randomSeed = 0;
    
    #endregion

    protected virtual void Awake()
    {
        Assert.IsTrue(gameObject!=null,"未找到游戏对象");
        rb = gameObject.GetComponent<Rigidbody>();
        originScale = transform.localScale.x;


    }

    protected void Start()
    {
        if (God.dev.ShowEnergy)
        {
            God.energyCanvasMgr.UpdateText(transform, Convert.ToString(Energy));
        }
    }


    protected void OnEnable()
    {
        randomSeed = Random.Range(0, 360);
        rb.velocity = Vector3.zero;
        sendFlag = true;
    }
    
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
        return rb.velocity;
    }

    #region 能量相关方法
    public void SetEnergy(float x)
    {
        Energy = x;
    }
    
    public void SetMaxEnergy(float x)
    {
        MaxEnergy = x;
    }

    public float AddEnergy(float x)
    {
        Energy = Mathf.Clamp(Energy + x, 0, MaxEnergy);
        return _energy;
    }
    
    #endregion



}
