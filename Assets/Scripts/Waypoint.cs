using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Waypoint : MonoBehaviour
{
    [SerializeField] Tower[] towerPrefab;
    
    private List<GameObject> towersActive = new List<GameObject>();
    
    [SerializeField] bool isPlacable;

    int index;
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
            index = 0;
            bool isPlaced =  towerPrefab[index].CreateTower(towerPrefab[index],transform.position, index);
            isPlacable = !isPlaced;
            // prosthetw sthn lista to towerPrefab[index] to thema einai ap...
            // Mhpws na kanw ena create manager kserw gw h kati ttoio
            // kai na yparxoyn ekei oles oi plirofories gia ta towers pou briskontai poio index exoun ktl
            // kalo, tha to skeftw 
        }
        
    }
}
