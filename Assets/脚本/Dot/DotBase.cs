using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotBase : MonoBehaviour
{
    protected ulong id;

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
}
