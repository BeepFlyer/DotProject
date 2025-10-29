using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackDot : DotBase
{
    private Rigidbody rb;
    public float randomSpeed = 1.0f;

    private MapConfig _mapConfig;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        _mapConfig = GameObject.Find("MapConfig").GetComponent<MapConfig>();
    }

    // Update is called once per frame
    void Update()
    {
        //ShowVector(transform.position,"before");
        transform.position = _mapConfig.GetNewPos(transform.position,rb.velocity);
        //ShowVector(transform.position,"after");
        

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
        
    }

}
