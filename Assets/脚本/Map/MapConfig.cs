using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class MapConfig : MonoBehaviour
{
    public float size_X = 100.0f;
    public float size_Y = 100.0f;

    private Vector3 rightTop = Vector3.one;

    private void Start()
    {
        rightTop = new Vector3(size_X / 2, size_Y / 2, 0);
    }

    public Vector3 GetNewPos(Vector3 pos,Vector3 vel)
    {
        if ((vel.x > 0 && pos.x > size_X)||(vel.x < 0 && pos.x < -size_X))
        {
            pos.x = -pos.x;
        }
        
        if ((vel.y > 0 && pos.y > size_Y)||(vel.y < 0 && pos.y < -size_Y))
        {
            pos.y = -pos.y;
        }

        return pos;
    }

    private void Update()
    {
        Debug.DrawLine(new Vector3(size_X / 2, size_Y / 2, 0)/2,new Vector3(-size_X / 2, size_Y / 2, 0)/2,Color.red,Time.deltaTime);
        Debug.DrawLine(new Vector3(-size_X / 2, size_Y / 2, 0)/2,new Vector3(-size_X / 2, -size_Y / 2, 0)/2,Color.red,Time.deltaTime);
        Debug.DrawLine(new Vector3(-size_X / 2, -size_Y / 2, 0)/2,new Vector3(size_X / 2, -size_Y / 2, 0)/2,Color.red,Time.deltaTime);
        Debug.DrawLine(new Vector3(size_X / 2, -size_Y / 2, 0)/2,new Vector3(size_X / 2, size_Y / 2, 0)/2,Color.red,Time.deltaTime);
    }
}
