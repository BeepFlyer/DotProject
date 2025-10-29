using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class DotManager
{
    private DotFactory factory;
    private BlackDotGravityManager black_gravity_mgr;
    private GameObject dotHolder;

    private float blackDotCollideMinSpeed = 10.0f; //黑点相撞质变最小速率

    public float GRAVITY_CAL_TIME = 1; //重力中心计算间隔
    
    /// <summary>
    /// 黑点重力中心
    /// </summary>
    public Vector3 GravityCenter
    {
        get
        {
            return _GravityCenter;
        }
        set
        {
            _GravityCenter = value;
        }
    }

    private Vector3 _GravityCenter = Vector3.zero;

    private Dictionary<ulong,DotBase> dotList = new Dictionary<ulong, DotBase>(500);

    public DotManager()
    {
        Debug.Log("DotManager初始化中");
        factory = new DotFactory();
        factory.SetDotManager(this);
        dotHolder = GameObject.Find("/dotsParent");
        factory.SetDotHolder(dotHolder);

        black_gravity_mgr = new BlackDotGravityManager();
        black_gravity_mgr.SetDotManager(this);
        black_gravity_mgr.Init();
        
        Debug.Log("DotManager初始化完成");

    }
    
    /// <summary>
    /// 根据点类型在某位置生成点
    /// </summary>
    /// <param name="type"></param>
    /// <param name="pos"></param>
    /// <returns></returns>
    public DotBase Spawn(DotType type,Vector3 pos)
    {
        DotBase script = factory.Spawn(type, pos);
        if (script != null)
        {
            dotList[script.GetId()] = script;
        }

        return script;
    }

    /// <summary>
    /// 去除点
    /// </summary>
    /// <param name="dot"></param>
    /// <returns></returns>
    public bool DeSpawn(DotBase dot)
    {
        dotList.Remove(dot.GetId());
        ObjectPool.Instance.ReturnObject((int)GlobalMapping.dotType2prefab[dot.Type],dot.gameObject);
        return true;
    }

    public bool TwoBlackDotCollide(DotBase script1, DotBase script2)
    {
        Vector3 speed1 = script1.GetSpeed();
        Vector3 speed2 = script2.GetSpeed();
        Vector3 sub = speed1 - speed2;
        if (sub.magnitude>=blackDotCollideMinSpeed)
        {
            // 达到质变速度
            Debug.Log("发生了质变碰撞");
            return true;
        }
        else
        {
            //未达到质变速度
            Debug.Log("发生了湮灭碰撞");
            DeSpawn(script1);
            DeSpawn(script2);
            return false;
        }
        

    }
    
    
    
    

}
