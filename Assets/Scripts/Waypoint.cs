using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Waypoint : MonoBehaviour
{
    [SerializeField] Tower[] towerPrefabs;

    [SerializeField] int cost = 75;
    
    [SerializeField] bool isPlaceable;
    [SerializeField] bool isUpgradeable;


    Bank bank;
    
    
    public bool IsPlaceable
    {
        get
        {
            return isPlaceable;
        }
        set
        {
            isPlaceable = value;
        }
        
    }
    public bool IsUpgradeable
    {
        get
        {
            return isUpgradeable;
        }
        set
        {
            isUpgradeable = value;
        }
        
    }

    
    void Start()
    {
        bank = FindObjectOfType<Bank>();
    }


    void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject() && isPlaceable && bank.CurrentBallance >= cost)
        {
            Actions.OnConstruction(this,0);
            return;
        }

        if (!EventSystem.current.IsPointerOverGameObject() && isUpgradeable)
        { 
            Actions.OnSelectAction(this);
            Actions.OnConstruction(this, 1);
        }
    }

  
    
    
    
    
    
    
    
    
    
    
    
    
    // void OnMouseDown()
    // {
    //     if (!EventSystem.current.IsPointerOverGameObject() && isUpgradeable)
    //     {
    //         Debug.Log("yo");
    //     }
    //     
    //     if (!EventSystem.current.IsPointerOverGameObject() && isPlacable)
    //     {
    //         bool isPlaced =  towerPrefab.CreateTower(towerPrefab,transform.position);
    //         isPlacable = !isPlaced;
    //         isUpgradeable = !isUpgradeable;
    //     }
    //
    // }
}
