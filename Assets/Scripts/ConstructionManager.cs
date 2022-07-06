using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionManager : MonoBehaviour
{

    [SerializeField] GameObject[] towerPrefabs;

    [SerializeField] int[] cost = { 75, 150, 300 };

    Vector3 position;
    List<GameObject> defencesOnScene = new List<GameObject>();
    GameObject temp;
    Waypoint tempWaypoint;

    Bank bank;

    int create = 0;
    int change = 1;

    public Tower tower;
    // int decision = 5;

    void Awake()
    {
        // Actions.OnCreateTower += CreateTowerHandler;
        Actions.OnUpgradeTower += UpgradeTowerHandler;
        // Actions.OnSellTower += SellTowerHandler;
        // Actions.OnGetPosition += GetPositionHandler;
        Actions.OnConstruction += ConstructionHandler;
    }

    void OnDestroy()
    {
        // Actions.OnCreateTower -= CreateTowerHandler;
        Actions.OnUpgradeTower -= UpgradeTowerHandler;
        // Actions.OnSellTower -= SellTowerHandler;
        // Actions.OnGetPosition -= GetPositionHandler;
        Actions.OnConstruction -= ConstructionHandler;
    }


    private void Start()
    {
        bank = FindObjectOfType<Bank>();
    }


    void ConstructionHandler(Waypoint waypoint, int action)
    {
        if (action == create)
        {
            CreateTower(waypoint);
        }

        if (action == change)
        {
            ChangeTower(waypoint);
        }

    }
    

    void CreateTower(Waypoint waypoint)
    {
        if (waypoint.IsPlaceable)
        {
            temp = Instantiate(towerPrefabs[0], waypoint.transform.position, Quaternion.identity);
            defencesOnScene.Add(temp);
            bank.Withdraw(cost[0]);
            waypoint.IsPlaceable = false;
            waypoint.IsUpgradeable = true;
        }
    }

    void ChangeTower(Waypoint waypoint)
    {
        for (int i = 0; i < defencesOnScene.Count; i++)
        {
            if (defencesOnScene[i].transform.position == waypoint.transform.position)
            {
                temp = defencesOnScene[i];
                
            }
        }
    }

    void UpgradeTowerHandler()
    {
        if (bank.CurrentBallance >= cost[1])
        {
            position = temp.transform.position;
            Destroy(temp);
            defencesOnScene.Remove(temp);
            temp = Instantiate(towerPrefabs[1], position, Quaternion.identity);
            bank.Withdraw(cost[1]);
            defencesOnScene.Add(temp);
        }
        else
        {
            Actions.OnNotEnoughGold();
        }
    }

}
