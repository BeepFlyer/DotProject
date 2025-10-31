using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.Assertions;

public class EnergyText : MonoBehaviour
{
    private Transform _aim;
    private TMP_Text text;
    /// <summary>
    /// 悬浮中物体上方的距离
    /// </summary>
    public Vector3 upDis = 1.0f *Vector3.up; 
    private RectTransform _rectTransform;

    private bool _debug = false;

    private void Awake()
    {
        text = GetComponent<TMP_Text>();
        _rectTransform = GetComponent<RectTransform>();
    }

    public void Init(Transform follow,string str = "")
    {
        text.text = str;
        _aim = follow;
        UpdatePos();
    }
    
    
    public void UpdateText(string txt)
    {
        text.text = txt;

    }

    
    
    
    private void Update()
    {
            if (_debug) Debug.Log("更新位置");
            UpdatePos();
    }

    private void UpdatePos()
    {
        transform.position = _aim.position+upDis;
        /*
        Vector3 screenPos = Camera.main.WorldToScreenPoint(_aim.position);
        Vector2 localPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _rectTransform,
            screenPos,
            Camera.main, 
            out localPos
        );
        _rectTransform.anchoredPosition = localPos +upDis;
    */
    }
}
