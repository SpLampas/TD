using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    private int id;
    // [SerializeField] int cost = 75;
    //
    //
    // public bool CreateTower(Tower tower, Vector3 position)
    // {
    //     Bank bank = FindObjectOfType<Bank>();
    //     if (bank == null)
    //     {
    //         return false;
    //     }
    //
    //     if (bank.CurrentBallance >= cost)
    //     {
    //         Instantiate(tower.gameObject, position, Quaternion.identity);
    //         bank.Withdraw(cost);
    //         return true;
    //     }
    //
    //     return false;
    // }
   

    public int SetTowerID()
    {
        if (CompareTag("SmallBallista"))
        {
            id = 1;
        }

        if (CompareTag("LargeBallista"))
        {
            id = 2;
        }
        if(CompareTag("MagicTower"))
        {
            id = 3;
        }
        return id;
    }

}
