using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainDot : DotBase
{
    public float randomSpeed = 1.0f;

    public AnimationCurve onClickCurve;
    private float maxAnimeTime = 0.5f;
    private bool _debug2 = false;

    private IDotPressBehaviour pressBehavior;

    public void SetPressBeahviour(IDotPressBehaviour x)
    {
        pressBehavior = x;
        if (x.GetType() == DotType.BlackDot)
        {
            BlackDotBahaviour y = x as BlackDotBahaviour;
            y.OnClickCurve = onClickCurve;
            y.MaxAnimeTime = maxAnimeTime;
        }

        base._debug = false;

    }
    

    void Update()
    {
        transform.position = God.mapConfig.GetNewPos(transform.position,rb.velocity);

        if (pressBehavior != null)
        {
            pressBehavior.OnDotUpdate(this);
        }
        /*
        if (nowAnimeTime < maxAnimeTime)
        {
            transform.localScale = Vector3.one* (onClickCurve.Evaluate((nowAnimeTime / maxAnimeTime))+originScale);
            nowAnimeTime += Time.deltaTime;
        }
        */

        Energy = rb.velocity.magnitude;
        if (_debug2)
        {
            Debug.Log($"改变能量{Energy}");
        }

    }
    
    public override void OnDotPressed()
    {
        base.OnDotPressed();
        if (pressBehavior != null)
        {
            pressBehavior.OnDotPressedEnter(this);
        }
        /*

        float angle = Random.Range(0f, 360f);
        Vector3 dir = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0f) * randomSpeed;
        rb.velocity += dir;
        
        nowAnimeTime = 0;
        */
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
                //dotScript.sendFlag = false;
                //God.dotManager.TwoBlackDotCollide(this, dotScript);
            }
            sendFlag = true;
        }
    }
}
