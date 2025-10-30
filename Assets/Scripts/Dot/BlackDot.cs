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

    

    void Update()
    {
        transform.position = God.mapConfig.GetNewPos(transform.position,rb.velocity);
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
