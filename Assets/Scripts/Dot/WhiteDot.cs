using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine;
using Random = UnityEngine.Random;

public class WhiteDot : DotBase
{
    private float spawnTime = 10.0f;

    private float spawnTimer;

    void Awake()
    {
        
    }
    
    void Start()
    {
        ResetTimer();
    }

    void Update()
    {
        UpdateTimer();
    }
    
    private void UpdateTimer()
    {
        if (spawnTimer > 0)
        {
            spawnTimer -= Time.deltaTime;
        }
        else
        {
            ResetTimer();
            TrySpawnBlackDot();
        }

        return;
    }

    private void TrySpawnBlackDot()
    {
        float angle = Random.Range(0f, Mathf.PI * 2f);
        Vector3 center = transform.position;
        float radius = .5f;
        // 计算圆上的点
        Vector3 spawnPosition = new Vector3();
        spawnPosition.x = center.x + Mathf.Cos(angle) * radius;
        spawnPosition.z = center.z + Mathf.Sin(angle) * radius;
        spawnPosition.y = center.y;

        ObjectPool.Instance.GetObject((int)GlobalMapping.dotType2prefab[dot.Type], spawnPosition, Quaternion.identity);
    }
    private void ResetTimer()
    {
        spawnTimer = spawnTime;
    }

}