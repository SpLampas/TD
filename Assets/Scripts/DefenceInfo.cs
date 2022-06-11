using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class DefenceInfo : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI displayInfo;

    public static Action<int,int> OnUpgrade;

    int amount;
    
    public void DisplayInfo(int level)
    {
        displayInfo.text = "Level: " + level;
    }

    public void UpgradeDefence()
    {
        
        OnUpgrade?.Invoke(0,amount);  //isws anti gia level na pernaw kateutheian to ammount
    }

    public void SellDefence()
    {
        OnUpgrade?.Invoke(1, amount); //isws anti gia level na pernaw kateutheian to ammount
    }
}
