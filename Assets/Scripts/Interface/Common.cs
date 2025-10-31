using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHasEnergy
{ 
    float Energy { get; set; }         // 当前能量
    float MaxEnergy { get; }
    void SetMaxEnergy(float amount);
    float AddEnergy(float amount);     // 增加能量（可返回当前能量）

}

public interface IReStartAble
{
    public void ReStart();
}