using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Waypoint : MonoBehaviour
{
    [SerializeField] Tower towerPrefab;
    
    [SerializeField] bool isPlacable;
    public bool IsPlaceable
    {
        get
        {
            return isPlacable;
        }
    }
    
    void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject() && isPlacable)
        {
            var idex = 0;
            bool isPlaced =  towerPrefab.CreateTower(towerPrefab,transform.position, idex);
            isPlacable = !isPlaced;
        }

        // if (!EventSystem.current.IsPointerOverGameObject() && Tower.)
        // {
        //     
        // }
    }
}
