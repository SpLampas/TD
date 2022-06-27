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
    

    private void Start()
    {
        bank = FindObjectOfType<Bank>();
    }


    void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject() && isPlaceable && bank.CurrentBallance >= cost)
        {
            var pos = transform.position;
            Actions.OnCreateTower(pos);
            CreatedTower();
            return;
        }

        if (!EventSystem.current.IsPointerOverGameObject() && isUpgradeable)
        {
            var pos = transform.position;
            Actions.OnSelectAction();
            Actions.OnGetPosition(pos);
        }
    }

   
    void CreatedTower()
    {
        isPlaceable = !isPlaceable;
        isUpgradeable = !isUpgradeable;
        
    }
}
