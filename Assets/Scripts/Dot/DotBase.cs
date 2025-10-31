using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Random = UnityEngine.Random;

public class DotBase : MonoBehaviour,IHasEnergy,IReStartAble,IUpdateAblePerS
{
    #region 属性
    protected ulong id;
    public Rigidbody rb;
    public float originScale = 1;
    protected bool _debug = false;

    /// <summary>
    /// 损失能量的速率，几秒钟损失1能量
    /// </summary>
    public int loseEnergyRateFactor = 3;

    private int loseEnergyTimer = 0;
    public float Energy
    {
        get { return _energy; }
        set
        {
            if (_debug)
            {Debug.Log($"尝试改变能量，旧{_energy}，新{value}");}

            if (Mathf.Abs(value - _energy) >Mathf.Epsilon)
            {
                if (_debug)
                {Debug.Log($"成功改变能量，旧{_energy}，新{value}");}
                _energy = value;
                OnEnergyChange();
            }
        }
    }
    private float _energy = 0;
    
    public float MaxEnergy
    {
        get { return _maxEnergy; }
        set { _maxEnergy = value; }
    }
    private float _maxEnergy = 10;

    
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

    public  float RandomSeed
    {
        get { return randomSeed; }
    }
    protected float randomSeed = 0;
    
    #endregion

    protected virtual void Awake()
    {
        Assert.IsTrue(gameObject!=null,"未找到游戏对象");
        rb = gameObject.GetComponent<Rigidbody>();
        originScale = transform.localScale.x;


    }

    public virtual void ReStart()
    {
        _energy = _maxEnergy;
        transform.localScale = originScale*Vector3.one;

        if (God.dev.ShowEnergy)
        {
            God.energyCanvasMgr.UpdateText(transform, _energy);
        }

        God.timeManager.AddToUpdate(this);

    }

    public virtual void OnReturnToPool()
    {
        God.timeManager.QuitUpdate(this);
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
        Energy = Mathf.Clamp(Energy + x, -10, MaxEnergy);
        return _energy;
    }

    protected void OnEnergyChange()
    {
        if (_energy < 0)
        {
                
            if (God.dev.ShowEnergy)
            {
                God.energyCanvasMgr.DotDie(transform);
            }

            God.dotManager.DeSpawn(this);

        }
        else
        {
            if (God.dev.ShowEnergy)
            {
                God.energyCanvasMgr.UpdateText(transform,_energy);
            }

        }
    }

    public void DoUpdatePerS()
    {
        if (loseEnergyTimer >= loseEnergyRateFactor)
        {
            loseEnergyTimer = 0;
            AddEnergy(-1);
        }
        else
        {
            loseEnergyTimer += 1;
        }
    }
    
    #endregion



}
