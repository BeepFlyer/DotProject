using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class MapConfig : MonoBehaviour
{
    public float size_X = 20.0f;
    public float size_Y = 20.0f;


    private void Start()
    {
    }

    public Vector3 GetNewPos(Vector3 pos,Vector3 vel)
    {
        bool change = false;
        if ((vel.x > 0 && pos.x > size_X/2)||(vel.x < 0 && pos.x < -size_X/2))
        {
            pos.x = -pos.x;
            change = true;
            Debug.Log(($"X change{-pos.x}to{pos.x}"));
        }
        
        if ((vel.y > 0 && pos.y > size_Y/2)||(vel.y < 0 && pos.y < -size_Y/2))
        {
            pos.y = -pos.y;
            change = true;
            Debug.Log(($"Y change{-pos.y}to{pos.y}"));
        }

        if (change)
        {
            Debug.Log("发生了change");
        }
        return pos;
    }

    private void Update()
    {
        Debug.DrawLine(new Vector3(size_X , size_Y , 0)/2,new Vector3(-size_X , size_Y , 0)/2,Color.red,Time.deltaTime);
        Debug.DrawLine(new Vector3(-size_X , size_Y , 0)/2,new Vector3(-size_X , -size_Y , 0)/2,Color.red,Time.deltaTime);
        Debug.DrawLine(new Vector3(-size_X , -size_Y , 0)/2,new Vector3(size_X , -size_Y , 0)/2,Color.red,Time.deltaTime);
        Debug.DrawLine(new Vector3(size_X , -size_Y , 0)/2,new Vector3(size_X , size_Y , 0)/2,Color.red,Time.deltaTime);
    }
}
