using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackDotGravityManager :IUpdateAble
{
    private DotManager _dotManager;
    
    private float timeNow =0;

    public void SetDotManager(DotManager manager)
    {
        _dotManager = manager;
    }

    public void Init()
    {
        God.timeManager.AddToUpdate(this);
    }

    public void DoUpdate(float deltaTime)
    {
        if (timeNow > _dotManager.GRAVITY_CAL_TIME)
        {
            timeNow = 0;
            CalGravityCenter();
        }
        else
        {
            timeNow += deltaTime;
        }
    }

    void CalGravityCenter()
    {
        
    }
    
    

    

}
