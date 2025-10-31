using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDotPressBehaviour
{
    public void OnDotPressedEnter(DotBase dot);
    
    public void OnDotUpdate(DotBase dot);

    public DotType GetType();

}