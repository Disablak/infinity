using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SetRadius : MonoBehaviour
{
    
    public UnitManager curUnit;
    public InfinityManager infinityManager;
    
    
    private void OnMouseDown()
    {
        if(infinityManager.curEvent == InfinityManager.Event.Move)
        {
            curUnit.PointToMove(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            transform.position = new Vector2(20, 20);
        }
    }
    
    
}
