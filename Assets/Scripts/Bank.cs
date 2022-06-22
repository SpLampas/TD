using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class Bank : MonoBehaviour
{
    [SerializeField] int startingBalance = 150;
    int currentBalance;
    [SerializeField] TextMeshProUGUI displayBalance;
    

    void Awake()
    {
        DefenceInfo.OnUpgrade += UpgradeHandler;
        currentBalance = startingBalance;
        UpdateDisplay();
    }

    private void OnDestroy()
    {
        DefenceInfo.OnUpgrade -= UpgradeHandler;
    }

    void UpgradeHandler(int i, int amount)
    {
        if (i == 0)
        {
            //upgrade => withdraw
        }

        if (i == 1)
        {
            //sell => deposit
        }
    }

    public int CurrentBallance
    {
        get { return currentBalance; }
    }

    public void Deposit(int ammount)
    {
        currentBalance += Mathf.Abs(ammount);
        UpdateDisplay();
    }
    
    public void Withdraw(int amount)
    {
        currentBalance -= Mathf.Abs(amount);
        UpdateDisplay();
        if (currentBalance < 0)
        {
            Time.timeScale = 0f;
            Actions.OnEmptyBank();
        }
    }

    void UpdateDisplay()
    {
        displayBalance.text = "Gold: " + currentBalance;
    }
    
   
}
