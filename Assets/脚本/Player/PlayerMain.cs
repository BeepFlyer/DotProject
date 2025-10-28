using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMain : MonoBehaviour
{
    public float speed = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HandleMove();
    }

    void HandleMove()
    {
        float xInput = Input.GetAxis("Horizontal");
        float yInput = Input.GetAxis("Vertical");
        
        transform.Translate(new Vector3(xInput,yInput,0)*speed*Time.deltaTime);
        
    }

    private void OnMouseDown()
    {
        Physics.Raycast(transform.position, Vector3.forward, 1000);
    }
}
