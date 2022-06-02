using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Waypoint : MonoBehaviour
{
    [SerializeField] Tower towerPrefab;//prepei na ginei pinakas
    
    [SerializeField] bool isPlacable;
    public bool IsPlaceable
    {
        get
        {
            return isPlacable;
        }
    }
    //isUpgradable opws pane
    void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject() && isPlacable)
        {
            var idex = 0;
            bool isPlaced =  towerPrefab.CreateTower(towerPrefab,transform.position, idex);
            isPlacable = !isPlaced;
        }
        else if(!EventSystem.current.IsPointerOverGameObject() && !isPlacable)
        {
            Debug.Log("Yo");
        }
    }
}
