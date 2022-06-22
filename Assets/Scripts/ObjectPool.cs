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
    // [Tooltip("Adds amount to maxHitPoints when Enemy dies.")]
    [SerializeField][Range(0,50)] int poolSize = 5;
    // [Tooltip("Adds amount to maxHitPoints when Enemy dies.")]
    [SerializeField] [Range(1f, 10f)] float timeBetweenWaves = 1f;

    [SerializeField] [Range(0, 100)] float enemy1Percentage;
    [SerializeField] [Range(0, 100)] float enemy2Percentage;
    [SerializeField] [Range(0, 100)] float enemy3Percentage;
    
// [Tooltip("Adds amount to maxHitPoints when Enemy dies.")]
    [SerializeField] [Range(0, 10)] int winCondition = 5;
    // [Tooltip("Adds amount to maxHitPoints when Enemy dies.")]
    [SerializeField] [Range(0, 10)] int extraEnemiesPerWave = 5;
    // [Tooltip("Adds amount to maxHitPoints when Enemy dies.")]
    [SerializeField] [Range(0, 1)] float looseCondition = 0.7f;
    
    List<GameObject> pool = new List<GameObject>();

    int waveCount = 1;
    
    int activeInSceneCount;
    int enemyReachedEnd = 0;
    int deadEnemy = 0;

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
        var per1 = Mathf.RoundToInt(poolSize * (enemy1Percentage/100));
        var per2 = Mathf.RoundToInt(poolSize * (enemy2Percentage/100));
        var per3 = Mathf.RoundToInt(poolSize * (enemy3Percentage/100));
        
        
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
        pool.Clear();
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
