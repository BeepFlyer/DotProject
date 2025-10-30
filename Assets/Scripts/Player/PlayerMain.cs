using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class PlayerMain : MonoBehaviour
{
    public float speed = 1.0f;

    public Rigidbody rb;
    private bool isInit = false;

    private MainDot mainDot;
    

/// <summary>
/// 设置相机，获取主点
/// </summary>
    public void Init()
    {
        if (isInit) return;
        Transform camtrans = Camera.main.gameObject.transform;
        camtrans.SetParent(transform);
        camtrans.localPosition = rb.transform.localPosition+10*Vector3.back;
        camtrans.LookAt(rb.transform);

        CameraMain cameraScript = camtrans.GetOrAddComponent<CameraMain>();
        cameraScript.followAim = rb.transform;
        cameraScript.normalFollow = true;
        

        mainDot = rb.gameObject.GetComponent<MainDot>();

        isInit = true;
    }

    public DotBase GetDot()
    {
        if (isInit)
        {
            mainDot = rb.gameObject.GetComponent<MainDot>();
        }
        return mainDot;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isInit) return;
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
        
        rb.MovePosition(rb.transform.position+speed*Time.deltaTime*(xInput*Vector3.right+yInput*Vector3.up));
        
    }

    private void MouseDown()
    {
        RaycastHit hit;
        Vector3 origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        bool hasHit = Physics.Raycast(origin, Vector3.forward, out hit, 1000);
        //Debug.Log($"HasHit{Convert.ToString(hasHit)}");
        if (hasHit)
        {
            //点到东西了
            hit.collider.SendMessage("OnDotPressed");
            Debug.DrawLine(origin,origin+50*Vector3.forward,Color.green,3.0f);
        }
        else
        {
            //没点到东西
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0;
            God.dotManager.Spawn(DotType.BlackDot, pos);
            Debug.DrawLine(origin,origin+50*Vector3.forward,Color.red,3.0f);
            

        }
    }
}
