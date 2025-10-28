using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackDot : DotBase
{
    private Rigidbody rb;
    public float randomSpeed = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public override void OnDotPressed()
    {
        base.OnDotPressed();
        float angle = Random.Range(0f, 360f);
        Vector3 dir = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0f) * randomSpeed;
        rb.velocity += dir;
        
    }

}
