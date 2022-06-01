using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMenus : MonoBehaviour
{
    [SerializeField] GameObject UI;
    [SerializeField] GameObject gameOver;
    [SerializeField] GameObject pause;

    void Awake()
    {
        Bank.OnEmptyBank += EmptyBankHandler;
        gameOver.SetActive(false);

    }

    void OnDestroy()
    {
        Bank.OnEmptyBank -= EmptyBankHandler;
    }

    void EmptyBankHandler()
    {
        gameOver.SetActive(true);
    }

    public void Pause()
    {
        pause.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        pause.SetActive(false);
        Time.timeScale = 1f;
    }
    
    public void QuitApp()
    {
        Application.Quit();
    }
    
    public  void Retry()
    {
        gameOver.SetActive(false);
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }
}
