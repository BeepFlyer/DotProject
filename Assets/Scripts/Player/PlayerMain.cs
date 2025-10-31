using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;


public class PlayerMain : MonoBehaviour,IReStartAble
{
    public float speed = 1.0f;

    public Rigidbody rb;
    private bool isInit = false;

    private MainDot mainDot;

    public Transform gravityTransform;

    private Transform camAim;
    private CameraMain cameraScript;

    public float testGravity = 1.0f;// 测试重力系数
    public float testDrag = 1.0f;// 测试阻力系数


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
            
            cameraScript = camtrans.GetOrAddComponent<CameraMain>();
            cameraScript.followAim = rb.transform;
            camAim = rb.transform;
            cameraScript.normalFollow = true;
            

            mainDot = rb.gameObject.GetComponent<MainDot>();
            mainDot.SetPressBeahviour(new BlackDotBahaviour());

            gravityTransform = transform.Find("gravityObj");

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


    public void ReStart()
    {
        
    }

    public void OnReturnToPool()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isInit) return;
        HandleMove();
        if (Input.GetMouseButton(0))
        {
            // 持续判断
            GenGravity();
        }
        else
        {

            if (cameraScript.followAim != rb.transform)
            {
                cameraScript.followAim = rb.transform;
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            MouseDown(); //按下瞬间
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
        RaycastHit? hitNull = ShootRay();
        bool hasHit = hitNull != null;
        
        Vector3 origin = Camera.main.ScreenToWorldPoint(Input.mousePosition); 

        //Debug.Log($"HasHit{Convert.ToString(hasHit)}");
        if (hasHit)
        {
            //点到东西了
            RaycastHit hit = (RaycastHit)hitNull;
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

    RaycastHit? ShootRay()
    {
        RaycastHit hit;
        Vector3 origin = Camera.main.ScreenToWorldPoint(Input.mousePosition); 
        bool hasHit = Physics.Raycast(origin, Vector3.forward, out hit, 1000);
        return hasHit ? hit : null;
    }



    private void GenGravity()
    {
        RaycastHit? hitNull = ShootRay();
        bool hasHit = hitNull != null;
        if (!hasHit)
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0;
            TestGravity(pos);
        }
        

    }
    private void TestGravity(Vector3 v)
    {
        gravityTransform.position = v;
        cameraScript.followAim = gravityTransform;
        camAim = gravityTransform;
        Vector3 sub = v - rb.transform.position;
        rb.AddForce(testGravity*sub.normalized*(1/(sub.magnitude)),ForceMode.Acceleration);
        rb.AddForce(rb.velocity.magnitude*rb.velocity.magnitude*(-1)*testDrag*rb.velocity.normalized,ForceMode.Acceleration);

    }
}
