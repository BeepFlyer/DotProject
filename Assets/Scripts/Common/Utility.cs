using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utility 
{
    /// <summary>
    /// 一个UI元素是否在相机中可以看见其任意一部分
    /// </summary>
    /// <param name="cam"></param>
    /// <param name="uiRect"></param>
    /// <returns></returns>
    public static bool IsUIVisibleInCamera(Camera cam, RectTransform uiRect)
    {
        if (cam == null || uiRect == null)
        { 
            Debug.LogError("有空有空");
            return false;
        }

        // 获取RectTransform的世界空间顶点
        Vector3[] worldCorners = new Vector3[4];
        uiRect.GetWorldCorners(worldCorners);

        foreach (Vector3 corner in worldCorners)
        {
            Vector3 viewPos = cam.WorldToViewportPoint(corner);

            if (viewPos.z > 0 &&
                (viewPos.x >= 0 && viewPos.x <= 1) &&
                (viewPos.y >= 0 && viewPos.y <= 1))
            {
                return true;
            }
        }

        return false;
    }
    
        public static  void ShowVector(Vector3 v,string prefix ="",GameObject go= null )
        {
            if (go)
            {
                Debug.Log(prefix+$"{go.name} pos x:{v.x} pos y:{v.y} pos z:{v.z}");

            }
            else
            {
                Debug.Log(prefix+$"pos x:{v.x} pos y:{v.y} pos z:{v.z}");

            }
        }


}
