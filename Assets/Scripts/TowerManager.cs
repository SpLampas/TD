using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    [SerializeField] int[] cost = new int[] { 75, 100 };

    bool isUpgradeable;


    public bool CreateTower(Tower tower, Vector3 position, int index)
    {
        Bank bank = FindObjectOfType<Bank>();
        if (bank == null)
        {
            return false;
        }

        if (index == 0)
        {
            isUpgradeable = true;
        }

        if (bank.CurrentBallance >= cost[index])
        {
            Instantiate(tower.gameObject, position, Quaternion.identity);
            bank.Withdraw(cost[index]);
            return true;
        }

        return false;

    }
}
