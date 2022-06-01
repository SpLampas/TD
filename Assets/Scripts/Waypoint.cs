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
            bool isPlaced =  towerPrefab.CreateTower(towerPrefab,transform.position);
            isPlacable = !isPlaced;
        }
        // if (isPlacable)
        // {
        //     bool isPlaced =  towerPrefab.CreateTower(towerPrefab,transform.position);
        //     isPlacable = !isPlaced;
        // }
        
    }
}
