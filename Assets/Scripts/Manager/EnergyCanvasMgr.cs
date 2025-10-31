using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class EnergyCanvasMgr 
{
    //只负责能量字样的创建和释放
    //维护一个字典，该字典用点的transform为键，用字样控制脚本作为值

    private Dictionary<Transform, EnergyText> dictionary = new Dictionary<Transform, EnergyText>();
    private GameObject _canvas;
    private bool _debug = true;

    /// <summary>
    /// 创建一个显示能量的专用画布
    /// </summary>
    public void Init()
    {
        Assert.IsNotNull(ObjectPool.Instance,"对象池未初始化");
        _canvas = ObjectPool.Instance.GetObject((int) PrefabEnum.EnergyCanvas, Vector3.zero, Quaternion.identity);
        
    }


    public void UpdateText(Transform trans, float energy = 0)
    {
        if (dictionary.ContainsKey(trans))
        {
            dictionary[trans].UpdateText(energy);
        }
        else
        {
            //创建新预制件
            GameObject temp = ObjectPool.Instance.GetObject((int) PrefabEnum.EnergyText, Vector3.zero, Quaternion.identity);
            EnergyText script = temp.GetComponent<EnergyText>();
            dictionary[trans] = script;
            if (_debug) Debug.Log($"成功将transform加入字典，名称:{trans.name}");
            temp.transform.SetParent(_canvas.transform);
            script.Init(trans,energy);
        }

    }

    public void DotDie(Transform trans)
    {
        if (dictionary.ContainsKey(trans))
        {
            EnergyText script = dictionary[trans];
            //归还预制件
            ObjectPool.Instance.ReturnObject((int)PrefabEnum.EnergyText,script.gameObject);
            dictionary.Remove(trans);
            if (_debug) Debug.Log($"成功回收能量文字 EnergyCanvasMgr，名称:{script.name}");

        }
        else
        {
            if (_debug) Debug.Log($"字典中未找到对应键EnergyCanvasMgr,，名称:{trans.name}");

        }

    }

    /*
    private void MoveOutOfScreen(Transform x)
    {
        if (_debug) Debug.Log($"触发归还，名称:{x.name}");

        if (dictionary.ContainsKey(x))
        {
            dictionary.Remove(x);
            if (_debug) Debug.Log($"成功回收EnergyCanvasMgr，名称:{x.name}");
            //归还预制件
            ObjectPool.Instance.ReturnObject((int)PrefabEnum.EnergyText,x.gameObject);
        }
        else
        {
            if (_debug) Debug.Log($"字典中未找到对应键EnergyCanvasMgr,，名称:{x.name}");

        }
    }
    */
    
}
