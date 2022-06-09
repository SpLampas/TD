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
    
    public static Action OnEmptyBank;

    void Awake()
    {
        currentBalance = startingBalance;
        UpdateDisplay();
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
            OnEmptyBank?.Invoke();
        }
    }

    void UpdateDisplay()
    {
        displayBalance.text = "Gold: " + currentBalance;
    }
    
   
}
