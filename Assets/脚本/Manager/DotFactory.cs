using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//它负责创建各种点
public class DotFactory
{
    private Dictionary<string, GameObject> spawnDict;

    private ulong dotId = 0;

    private GameObject dotHolder;

    private DotManager _dotManager;

    public DotFactory()
    {
        spawnDict = new Dictionary<string, GameObject>()
        {
            ["Black"] = Resources.Load<GameObject>("预制件/点/blackDot")
        };
        Debug.Log("DotFactory初始化完成");
    }


    public void SetDotManager(DotManager manager)
    {
        _dotManager = manager;
    }
    
    public void SetDotHolder(GameObject dotHolder)
    {
        this.dotHolder = dotHolder;
    }

    public DotBase Spawn(string typeName)
    {
        if (spawnDict.ContainsKey(typeName))
        {
            if (!dotHolder)
            {
                Debug.LogError("没找到点存放处");
                return null;
            }

            GameObject dotObj = spawnDict[typeName];
            if (!dotObj ) Debug.LogError("字典无点"+typeName);
            GameObject go = GameObject.Instantiate(dotObj, Vector3.zero, Quaternion.identity);
            go.transform.parent = dotHolder.transform;
            DotBase script = go.GetOrAddComponent<DotBase>();
            script.SetId(dotId);
            dotId++;
            return script;

        }
        else
        {
            Debug.LogError("没有找到对应预制件："+typeName);
            return null;
        }
    }
    
    public DotBase Spawn(string typeName,Vector3 pos)
    {
        DotBase script=this.Spawn(typeName);
        script.transform.position = pos;
        return script;
    }

}
