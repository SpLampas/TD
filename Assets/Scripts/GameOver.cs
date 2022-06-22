using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    void Awake()
    {
        Actions.OnEmptyBank += EmptyBankHandler;
        gameObject.SetActive(false);

    }

    void OnDestroy()
    {
        Actions.OnEmptyBank -= EmptyBankHandler;
    }

    void EmptyBankHandler()
    {
        gameObject.SetActive(true);
    }
}
