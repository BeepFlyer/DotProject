using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine;
using Random = UnityEngine.Random;

public class WhiteDot : DotBase
{
    private float spawnTime = 10.0f;

    private float spawnTimer;
    private List<GameObject> haveDots = new List<GameObject>();
    void Awake()
    {
        base.Awake();
    }
    
    public override void ReStart()
    {
        base.ReStart();
        ResetTimer();
    }

    void Update()
    {
        UpdateTimer();
        transform.position = God.mapConfig.GetNewPos(transform.position,rb.velocity);
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
        float radius = 5f;
        // 计算圆上的点
        Vector3 spawnPosition = new Vector3();
        spawnPosition.x = center.x + Mathf.Cos(angle) * radius;
        spawnPosition.z = center.z + Mathf.Sin(angle) * radius;
        spawnPosition.y = center.y;

        var newDot =  ObjectPool.Instance.GetObject((int)GlobalMapping.dotType2prefab[DotType.BlackDot], spawnPosition, Quaternion.identity);
        this.haveDots.Append(newDot);
        PushDot(newDot);
    }
    private void ResetTimer()
    {
        spawnTimer = spawnTime;
    }

    private void PushDot(GameObject dot)
    {
        Rigidbody rb = dot.GetComponent<Rigidbody>();

        Vector3 diretion = dot.transform.position - this.transform.position;
        rb.AddForce(diretion, ForceMode.Impulse);
    }
}