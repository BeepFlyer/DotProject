using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class BlackDotBahaviour : IDotPressBehaviour
{
    public float RandomSpeed = 1.0f;

    public AnimationCurve OnClickCurve;
    public float MaxAnimeTime = 0.5f;
    private float _nowAnimeTime = 0.5f;



    public void OnDotPressedEnter(DotBase dot)
    {
        /*
        float angle = Random.Range(0f, 360f);
        Vector3 dir = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0f) * RandomSpeed;
        */
        Vector3 origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        origin.z = 0;
        Vector3 dir = (dot.transform.position - origin).normalized * dot.RandomSeed/36;
        dot.rb.velocity += dir;
        _nowAnimeTime = 0;


    }

    public void OnDotUpdate(DotBase dot)
    {
        dot.transform.position = God.mapConfig.GetNewPos(dot.transform.position,dot.rb.velocity);
        if (_nowAnimeTime < MaxAnimeTime)
        {
            dot.transform.localScale = Vector3.one* (OnClickCurve.Evaluate((_nowAnimeTime / MaxAnimeTime))+dot.originScale);
            _nowAnimeTime += Time.deltaTime;
        }

    }

    public DotType GetType()
    {
        return DotType.BlackDot;
    }

}
