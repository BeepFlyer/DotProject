using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class DebugUI : MonoBehaviour
{
    [SerializeField] private Button SpawnWhiteDotBtn;


    private void Awake()
    {
        SpawnWhiteDotBtn.onClick.AddListener(() =>
        {
            ObjectPool.Instance.GetObject((int)PrefabEnum.WhiteDot, Vector3.zero, quaternion.identity);
        });
    }
}
