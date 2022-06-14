using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIMenus : MonoBehaviour
{
    [SerializeField] GameObject gameOver;
    [SerializeField] GameObject pause;
    [SerializeField] GameObject levelSelector;
    [SerializeField] GameObject stageCleared;
    [SerializeField] GameObject waves;
    [SerializeField] TextMeshProUGUI displayWave;

    float slowdownFactor =0.05f;
    float slowdownLength = 3f;

    IEnumerator coroutine;

    Bank bank;
    int sloMoGoldPenalty = 50;
    
    void Awake()
    {
        Bank.OnEmptyBank += EmptyBankHandler;
        ObjectPool.OnNewWave += NewWaveHandler;
        ObjectPool.OnStageClear += StageClearHandler;
        Time.timeScale = 1f;

    }

    void Start()
    {
        bank = FindObjectOfType<Bank>();
    }

    void OnDestroy()
    {
        Bank.OnEmptyBank -= EmptyBankHandler;
        ObjectPool.OnNewWave -= NewWaveHandler;
        ObjectPool.OnStageClear -= StageClearHandler;
    }

    void EmptyBankHandler()
    {
        gameOver.SetActive(true);
    }

    void StageClearHandler()
    {
        stageCleared.SetActive(true);
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
        coroutine = ReturnToRealTime(1f,2f);
        StartCoroutine(coroutine);
        if (bank == null)
        {
            return;
        }
        bank.Withdraw(sloMoGoldPenalty);
    }

    IEnumerator ReturnToRealTime(float lerpTimeTo,float timeToTake)
    {
        float endTime = Time.time + slowdownLength;
        float startTimeScale = Time.timeScale;
        float i = slowdownFactor;
        while (Time.time < endTime)
        {
            if (pause.activeInHierarchy || levelSelector.activeInHierarchy)
            {
                yield break;
            }
            i += (1f / timeToTake) * Time.unscaledDeltaTime; 
            Time.timeScale = Mathf.Lerp(startTimeScale, lerpTimeTo, i); 
            // print(Time.timeScale);
            yield return null;
           
        }
        Time.timeScale = lerpTimeTo;
        // Time.timeScale += (1f / slowdownLength) * Time.unscaledDeltaTime;
        // Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
    }


    public void SelectStage()
    {
        levelSelector.SetActive(true);
        Time.timeScale = 0f;
    }

    public void CloseTab()
   {
       levelSelector.SetActive(false);
       Time.timeScale = 1f;
   }

    public void LoadStage(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }


    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


    void NewWaveHandler(int waveCount, float timeBetweenWaves)
    {
        var waveNumber = waveCount + 1;
        StartCoroutine(DisplayNewWaves(waveNumber,timeBetweenWaves));
    }

    IEnumerator DisplayNewWaves(int waveNumber, float timeBetweenWaves)
    {
        waves.gameObject.SetActive(true);
        displayWave.text = "Wave: " + waveNumber;
        yield return new WaitForSeconds(timeBetweenWaves);
        waves.gameObject.SetActive(false);
    }
    
}
