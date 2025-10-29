using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Security;
using UnityEngine;
using UnityEngine.Pool;
using Object = System.Object;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance { get; private set; }
    
    [System.Serializable]
    public struct PoolPrefab
    {
        public int typeId;
        public GameObject prefab;
    }

    public PoolPrefab[] prefabs;

    // 实际的对象池（类型ID -> 队列）
    private Dictionary<int, Queue<GameObject>> poolDict;
    private Dictionary<int, GameObject> prefabDict;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            poolDict = new Dictionary<int, Queue<GameObject>>();
            prefabDict = new Dictionary<int, GameObject>();

            foreach (var item in prefabs)
            {
                prefabDict[item.typeId] = item.prefab;
                poolDict[item.typeId] = new Queue<GameObject>();
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// 从对象池中获取一个对象
    /// </summary>
    public GameObject GetObject(int typeId, Vector3 position, Quaternion rotation)
    {
        if (!poolDict.ContainsKey(typeId))
        {
            Debug.LogError($"对象池中没有类型ID为 {typeId} 的预制体！");
            return null;
        }

        GameObject obj;
        if (poolDict[typeId].Count > 0)
        {
            obj = poolDict[typeId].Dequeue();
            obj.SetActive(true);
        }
        else
        {
            obj = Instantiate(prefabDict[typeId]);
        }

        obj.transform.position = position;
        obj.transform.rotation = rotation;
        return obj;
    }

    /// <summary>
    /// 将对象回收到池中
    /// </summary>
    public void ReturnObject(int typeId, GameObject obj)
    {
        if (!poolDict.ContainsKey(typeId))
        {
            Debug.LogWarning($"回收失败：没有ID {typeId} 的对象池！");
            Destroy(obj);
            return;
        }

        obj.SetActive(false);
        poolDict[typeId].Enqueue(obj);
    }

}