using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;

public class CameraMain : MonoBehaviour
{
    public Transform followAim;
    public bool normalFollow = false;
    public float followSpeed = 1;
    private Vector3 normalHeight = Vector3.back*10;


    private void Update()
    {
        if (normalFollow && followAim)
        {
            
            transform.position += (followAim.position - transform.position+normalHeight) * followSpeed * Time.deltaTime;
        }
            
    }
}
