using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

public class BlackDot : DotBase
{
    public float randomSpeed = 1.0f;

    public AnimationCurve onClickCurve;
    private float maxAnimeTime = 0.5f;
    private float nowAnimeTime = 0.5f;
    private float originScale = 1;

    

    private MapConfig _mapConfig;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        _mapConfig = GameObject.Find("MapConfig").GetComponent<MapConfig>();
        originScale = transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        //ShowVector(transform.position,"before");
        Assert.IsTrue(transform != null, "未找到transform");
        Assert.IsTrue(rb!=null,"未找到刚体组件");
        transform.position = _mapConfig.GetNewPos(transform.position,rb.velocity);
        //ShowVector(transform.position,"after");
        if (nowAnimeTime < maxAnimeTime)
        {
            transform.localScale = Vector3.one* (onClickCurve.Evaluate((nowAnimeTime / maxAnimeTime))+originScale);
            nowAnimeTime += Time.deltaTime;
        }

    }

    void ShowVector(Vector3 v,string str)
    {
        Debug.Log(str+$"{gameObject.name} pos x:{v.x} pos y:{v.y} pos z:{v.z}");
    }
    
    public override void OnDotPressed()
    {
        base.OnDotPressed();
        float angle = Random.Range(0f, 360f);
        Vector3 dir = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0f) * randomSpeed;
        rb.velocity += dir;
        
        nowAnimeTime = 0;
    }

    public override Vector3 GetSpeed()
    {
        return rb.velocity;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!sendFlag)
        {
            sendFlag = true;
            return;
        }
        if (other.collider.gameObject.CompareTag("Dot"))
        {
            DotBase dotScript = other.collider.GetComponent<DotBase>();
            DotType colliderDotType = dotScript.Type;
            if (colliderDotType == DotType.BlackDot)
            {
                dotScript.sendFlag = false;
                God.dotManager.TwoBlackDotCollide(this, dotScript);
            }
            sendFlag = true;
        }
    }
}
