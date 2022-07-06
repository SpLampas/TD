using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] GameObject[] enemyprefab;
    // [Tooltip("Adds amount to maxHitPoints when Enemy dies.")]
    [SerializeField][Range(0.1f, 30f)] float spawnTimer = 1f;
    
    [Tooltip("Number of enemies at the beginning of the stage.")]
    [SerializeField][Range(0,50)] int poolSize = 5;
    
    [Tooltip("Time between spawning waves.")]
    [SerializeField] [Range(1f, 10f)] float timeBetweenWaves = 1f;

    [SerializeField] [Range(0, 100)] float enemy1Percentage;
    [SerializeField] [Range(0, 100)] float enemy2Percentage;
    [SerializeField] [Range(0, 100)] float enemy3Percentage;
    
    [Tooltip("Number of waves need to defeat for Stage Clear.")]
    [SerializeField] [Range(0, 10)] int winCondition = 5;
    
    [Tooltip("Adds extra enemies every wave.")]
    [SerializeField] [Range(0, 10)] int extraEnemiesPerWave = 5;
    
    [Tooltip("Percentage of enemies that if survive, the game is over.")]
    [SerializeField] [Range(0, 1)] float looseCondition = 0.7f;
    
    List<GameObject> pool = new List<GameObject>();

    int waveCount = 1;
    
    int activeInSceneCount;
    int enemyReachedEnd = 0;
    // int deadEnemy = 0;

    IEnumerator prepareForWave;
    
    
    void Awake()
    {
        PopulatePool(poolSize);
        Actions.OnDeath += OnDeathHandler;
    }

    void OnDestroy()
    {
        Actions.OnDeath -= OnDeathHandler;
    }

    void OnDeathHandler(int deathCount)
    {
        if (deathCount == 0)
        {
            enemyReachedEnd++;
        }
        
        activeInSceneCount--;
     
        if (activeInSceneCount == 0)
        {
            prepareForWave = PrepareForWave();
            StartCoroutine(prepareForWave);
        }

        if (enemyReachedEnd >= looseCondition * poolSize)
        {
            Actions.OnEnemyReached();
        }
    }

   
    void Start()
    {
        StartCoroutine(SpawnEnemy());
        activeInSceneCount = poolSize;
    }

 
    void PopulatePool( int poolSize)
    {
        var per1 = CalculatePercentage(poolSize,enemy1Percentage);
        var per2 = CalculatePercentage(poolSize,enemy2Percentage);
        var per3 = CalculatePercentage(poolSize,enemy3Percentage);
        
        
        for (int i = 0; i < per1; i++)
        {
            var tempEnemy= Instantiate(enemyprefab[0], transform);
            tempEnemy.SetActive(false);
            pool.Add(tempEnemy);
        }
        for (int i = 0; i < per2; i++)
        {
            var tempEnemy= Instantiate(enemyprefab[1], transform);
            tempEnemy.SetActive(false);
            pool.Add(tempEnemy);
        }
        for (int i = 0; i < per3; i++)
        {
            var tempEnemy= Instantiate(enemyprefab[2], transform);
            tempEnemy.SetActive(false);
            pool.Add(tempEnemy);
        }
    }

    int CalculatePercentage(float size, float percentage)
    {
        var value = Mathf.RoundToInt(size * (percentage/100));
        return value;
    }

    IEnumerator SpawnEnemy()
    {
        for (int i = 0; i < pool.Count; i++)
        {
            if (!pool[i].activeInHierarchy)
            {
                pool[i].SetActive(true);
            }
            
            yield return new WaitForSeconds(spawnTimer);
        }

        // foreach (var enemy in pool)
        // {
        //     enemy.SetActive(true);
        //     yield return new WaitForSeconds(spawnTimer);
        // }
    }

   
    IEnumerator PrepareForWave()
    {
        if (waveCount == winCondition)
        {
            Actions.OnStageClear();
            StopCoroutine(prepareForWave);
        }
        Actions.OnNewWave(waveCount, timeBetweenWaves);
        yield return new WaitForSeconds(timeBetweenWaves);
        StartWave();
    }
    
     

    void StartWave()
    {
        enemyReachedEnd = 0;
        waveCount++;
        poolSize += extraEnemiesPerWave;
        // Debug.Log($"<color=yellow>START WAVE poolSize: </color> {poolSize}");
        // pool.Clear();
        PopulatePool(poolSize); 
        StartCoroutine(SpawnEnemy());
        activeInSceneCount = poolSize;
        
    }


    
    
    // void EnableObjectInPool()
    // {
    //     for (int i = 0; i < pool.Length; i++)
    //     {
    //         if (pool[i].activeInHierarchy == false)
    //         {
    //             pool[i].SetActive(true);
    //             return;
    //         }
    //     }
    // }
    
  
}
