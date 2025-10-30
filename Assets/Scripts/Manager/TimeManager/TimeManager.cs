using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private List<IUpdateAble> list = new List<IUpdateAble>();
    public void Update()
    {
        float time = Time.deltaTime;
        foreach (var one in list)
        {
            one.DoUpdate(time);
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
    
    
}
