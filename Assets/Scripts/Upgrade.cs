using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    [SerializeField] Tower towerPrefab;
    [SerializeField] bool isUpgradable;
    [SerializeField] int cost;
    Bank bank;
    private void Start()
    {
        isUpgradable = true;
        bank = FindObjectOfType<Bank>();
    }

    void OnMouseDown()
    {
        if (isUpgradable && bank.CurrentBallance>= cost)
        {
            // bool isUpgratable =  towerPrefab.CreateTower(towerPrefab,transform.position);
            // isUpgradable = !isPlaced;
        }
        
    }
}
