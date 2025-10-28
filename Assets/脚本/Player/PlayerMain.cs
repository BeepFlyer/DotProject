using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMain : MonoBehaviour
{
    public float speed = 1.0f;

    public GameObject blackDot;

    private GameObject dotsParent;
    // Start is called before the first frame update
    void Start()
    {
        dotsParent = GameObject.Find("dotsParent");
    }

    // Update is called once per frame
    void Update()
    {
        HandleMove();
        if (Input.GetMouseButtonDown(0))
        {
            MouseDown();
        }
    }

    void HandleMove()
    {
        float xInput = Input.GetAxis("Horizontal");
        float yInput = Input.GetAxis("Vertical");
        
        transform.Translate(new Vector3(xInput,yInput,0)*speed*Time.deltaTime);
        
    }

    private void MouseDown()
    {
        RaycastHit hit;
        bool hasHit = Physics.Raycast(transform.position, Vector3.forward, out hit, 1000);
        //Debug.Log($"HasHit{Convert.ToString(hasHit)}");
        if (hasHit)
        {
            //点到东西了
            hit.collider.SendMessage("OnDotPressed");
        }
        else
        {
            //没点到东西
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0;
            GameObject go = Instantiate(blackDot, pos, Quaternion.identity);
            go.transform.parent = dotsParent.transform;
        }
    }
}
