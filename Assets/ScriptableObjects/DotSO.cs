using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDotData", menuName = "New Dot Data")]
public class DotSO : ScriptableObject
{
    public int TypeId;
    public GameObject Prefab;
    public string TypeName;
}
