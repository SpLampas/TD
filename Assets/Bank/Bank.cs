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
    
    public void Withdraw(int ammount)
    {
        currentBalance -= Mathf.Abs(ammount);
        UpdateDisplay();
        if (currentBalance < 0)
        {
            ReloadScene();
        }
    }

    void UpdateDisplay()
    {
        displayBalance.text = "Gold: " + currentBalance;
    }
    
    void ReloadScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }
}
