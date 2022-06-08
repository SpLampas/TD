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

    float slowdownFactor =0.05f;
    float slowdownLength = 3f;
    void Awake()
    {
        Bank.OnEmptyBank += EmptyBankHandler;
        gameOver.SetActive(false);
        Time.timeScale = 1f;

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

    public void SlowMotion()
    {
        Time.timeScale = slowdownFactor;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
        StartCoroutine(ReturnToRealTime());
    }

    IEnumerator ReturnToRealTime()
    {
        Time.timeScale += (1f / slowdownLength) * Time.unscaledDeltaTime;
        Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
        yield return new WaitForEndOfFrame();
    }
}
