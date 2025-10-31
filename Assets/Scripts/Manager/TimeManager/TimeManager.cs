using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private List<IUpdateAble> list = new List<IUpdateAble>();
    
    private List<IUpdateAblePerS> listPerS = new List<IUpdateAblePerS>();

    
    private float timePerS=0;
    public void Update()
    {
        float time = Time.deltaTime;
        foreach (var one in list)
        {
            one.DoUpdate(time);
        }

        timePerS += Time.deltaTime;
        if (timePerS >= 1)
        {
            timePerS = 0;
            //由于在更新点的时候，点可能会消亡，点消亡时会注销在TimeManager中的每秒调用
            //会更改listPerS，所以用temp列表进行遍历
            List<IUpdateAblePerS> temp = new List<IUpdateAblePerS>();
            foreach (var one in listPerS)
            {
                temp.Add(one);
            }
            foreach (var one in temp)
            {
                one.DoUpdatePerS();
            }


        }
        
        
    }

    public bool AddToUpdate(IUpdateAble one)
    {
        if (list.Contains(one))
        {
            return false;
        }
        else
        {
            list.Add(one);
            return true;
        }
    }
    
    public bool AddToUpdate(IUpdateAblePerS one)
    {
        if (listPerS.Contains(one))
        {
            return false;
        }
        else
        {
            listPerS.Add(one);
            return true;
        }
    }

    public void QuitUpdate(IUpdateAblePerS one)
    {
        listPerS.Remove(one);
    }

    
    
}
