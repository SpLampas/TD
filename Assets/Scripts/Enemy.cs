using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int rewardGold = 25;
    [SerializeField] int penaltyGold = 25;

    Bank bank;
    

    void Start()
    {
        bank = FindObjectOfType<Bank>();
    }

    public void RewardGold()
    {
        if (bank == null)
        {
            return;
        }
        bank.Deposit(rewardGold);
    }

    public void PenaltyGold()
    {
        if (bank == null)
        {
            return;
        }
        bank.Withdraw(penaltyGold);
    }
   
}
