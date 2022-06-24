using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Upgrade : MonoBehaviour
{
    [SerializeField] private List<GameObject> towers = new List<GameObject>();

    [SerializeField] private List<int> towerCost = new List<int>(){ 75, 150, 300 };
    
    [SerializeField] List<Waypoint> worldTiles = new List<Waypoint>();

    Bank bank;

    void Start()
    {
        bank = FindObjectOfType<Bank>();
    }

    public void UpgradeTower(int towerPositionInList)
    {
        
        for (int i = 0; i < towers.Count; i++)
        {
            if (towerPositionInList == i)
            {
                if (bank.CurrentBallance >= towerCost[towerPositionInList + 1])
                {
                    Destroy(towers[i]);
                    Actions.OnMakePlacableAgain(towers[i].transform.position);
                    Instantiate(towers[i + 1],towers[i].transform.position,Quaternion.identity);
                    var cost = -towerCost[i +1];
                    Actions.OnChangeTower(cost);
                }
                else
                {
                    Actions.OnNotEnoughGold();
                }
               
            }
            
        }
    }

    public void SellTower(int towerPositionInList)
    {
       
        for (int i = 0; i < towers.Count; i++)
        {
            if (towerPositionInList == i)
            {
                Destroy(towers[i]);
                var cost = towerCost[i]/2;
                Actions.OnChangeTower(cost);
            }
        }
        for (int i = 0; i < worldTiles.Count; i++)
        {
            if (worldTiles[i].transform.position == towers[towerPositionInList].transform.position)
            {
                worldTiles[i].IsPlaceable = true;
                break;
            }  
        }
    }
}
