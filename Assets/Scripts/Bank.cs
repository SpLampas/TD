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

    // private bool canUpgrade = true;
    
    void Awake()
    {
        Actions.OnChangeTower += ChangeTowerHandler;
        currentBalance = startingBalance;
        UpdateDisplay();
    }

    void OnDestroy()
    {
        Actions.OnChangeTower -= ChangeTowerHandler;
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

    void ChangeTowerHandler(int ammount)
    {
        currentBalance += ammount;
        UpdateDisplay();
    }
    
    void UpdateDisplay()
    {
        displayBalance.text = "Gold: " + currentBalance;
    }


   


}
