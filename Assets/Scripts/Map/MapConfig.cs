using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;
using NaughtyAttributes;
using Vector2 = UnityEngine.Vector2;

public class MapConfig : MonoBehaviour
{
    private float _size_X = 20.0f;
    private float _size_Y = 20.0f;
    

    public float size_X
    {
        get { return _size_X; }
        set { _size_X = value;
            ChangeBorder();
        }
    }

    public float size_Y
    {
        get { return _size_Y; }
        set
        {
            _size_Y = value;
            ChangeBorder();
        }
    }

    
    private LineRenderer line;

    private void Start()
    {
        line = transform.Find("border").GetOrAddComponent<LineRenderer>();
        InitBorder();
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
        /*
        Debug.DrawLine(new Vector3(size_X , size_Y , 0)/2,new Vector3(-size_X , size_Y , 0)/2,Color.red,Time.deltaTime);
        Debug.DrawLine(new Vector3(-size_X , size_Y , 0)/2,new Vector3(-size_X , -size_Y , 0)/2,Color.red,Time.deltaTime);
        Debug.DrawLine(new Vector3(-size_X , -size_Y , 0)/2,new Vector3(size_X , -size_Y , 0)/2,Color.red,Time.deltaTime);
        Debug.DrawLine(new Vector3(size_X , -size_Y , 0)/2,new Vector3(size_X , size_Y , 0)/2,Color.red,Time.deltaTime);
        */
    }

    

    void InitBorder()
    {
        SetBorder();
        line.positionCount = 5; // 矩形 4 个顶点 + 回到起点闭合
        line.loop = false; // loop = false, 我们手动闭合
        line.useWorldSpace = false; // 相对于物体本身
        line.startWidth = 0.3f;
        line.endWidth = 0.3f;
        line.startColor = Color.green;
        line.endColor = Color.blue;
        ChangeBorder();
    }
    void ChangeBorder()
    {
        Vector3[] corners = new Vector3[5];

        // 四个角点（跟你的 Debug.DrawLine 一致）
        corners[0] = new Vector3(size_X, size_Y, 0) / 2;
        corners[1] = new Vector3(-size_X, size_Y, 0) / 2;
        corners[2] = new Vector3(-size_X, -size_Y, 0) / 2;
        corners[3] = new Vector3(size_X, -size_Y, 0) / 2;
        corners[4] = corners[0]; // 回到起点，闭合矩形

        line.SetPositions(corners);

    }




    public Vector2 Border = Vector2.one;
    [Button("设置边界")]
    void SetBorder()
    {
        size_X = Border.x;
        size_Y = Border.y;
    }
    
}
