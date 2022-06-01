using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    void Awake()
    {
        Bank.OnEmptyBank += EmptyBankHandler;
        gameObject.SetActive(false);

    }

    void OnDestroy()
    {
        Bank.OnEmptyBank -= EmptyBankHandler;
    }

    void EmptyBankHandler()
    {
        gameObject.SetActive(true);
    }
}
