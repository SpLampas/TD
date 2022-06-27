using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionManager : MonoBehaviour
{
    
    [SerializeField] GameObject[] towerPrefabs;

    [SerializeField] int cost = 75;
    

    Vector3 position;
    private List<GameObject> defencesOnScene = new List<GameObject>();
    GameObject temp;

    Bank bank;
    
    void Awake()
    {
        Actions.OnCreateTower += CreateTowerHandler;
        Actions.OnUpgradeTower += UpgradeTowerHandler;
        // Actions.OnSellTower += SellTowerHandler;
        Actions.OnGetPosition += GetPositionHandler;
    }

    void OnDestroy()
    {
        Actions.OnCreateTower -= CreateTowerHandler;
        Actions.OnUpgradeTower -= UpgradeTowerHandler;
        // Actions.OnSellTower -= SellTowerHandler;
        Actions.OnGetPosition -= GetPositionHandler;
    }


    private void Start()
    {
        bank = FindObjectOfType<Bank>();
    }

    void CreateTowerHandler(Vector3 pos)
    {
        temp = Instantiate(towerPrefabs[0], pos, Quaternion.identity);
        defencesOnScene.Add(temp);
        bank.Withdraw(cost);
    }

    void UpgradeTowerHandler()
    {
        Destroy(temp);
        defencesOnScene.Remove(temp);
        temp = Instantiate(towerPrefabs[1], position, Quaternion.identity);
        defencesOnScene.Add(temp);

    }

    void SellTowerHandler()
    {
        
    }

    void GetPositionHandler(Vector3 pos)
    {
        position = pos;
        for (int i = 0; i < defencesOnScene.Count; i++)
        {
            if (defencesOnScene[i].transform.position == position)
            {
                temp = defencesOnScene[i];
                return;
            }
        }
    }
}
