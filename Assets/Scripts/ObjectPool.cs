using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] GameObject[] enemyprefab;
    [SerializeField][Range(0.1f, 30f)] float spawnTimer = 1f;
    [SerializeField][Range(0,50)] int poolSize = 5;
    
    [SerializeField] [Range(1f, 10f)] float timeBetweenWaves = 1f;

    [SerializeField] [Range(0, 100)] float enemy1Percentage;
    [SerializeField] [Range(0, 100)] float enemy2Percentage;
    [SerializeField] [Range(0, 100)] float enemy3Percentage;

    [SerializeField] int winCondition = 5;

    List<GameObject> pool = new List<GameObject>();

    int waveCount = 1;
    
    int activeInSceneCount;

    IEnumerator prepareForWave;

    public static Action<int, float> OnNewWave;
    public static Action OnStageClear;
 
    void Awake()
    {
        PopulatePool(poolSize);
        EnemyHealth.OnDeath += OnDeathHandler;
    }

    void OnDestroy()
    {
        EnemyHealth.OnDeath -= OnDeathHandler;
    }

    void OnDeathHandler(int deathCount)
    {
        activeInSceneCount += deathCount;
        if (activeInSceneCount == 0)
        {
            prepareForWave = PrepareForWave();
            StartCoroutine(prepareForWave);
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
            pool[i].SetActive(true);
            yield return new WaitForSeconds(spawnTimer);
        }
    }

   
    IEnumerator PrepareForWave()
    {
        if (waveCount == winCondition)
        {
            OnStageClear?.Invoke();
            StopCoroutine(prepareForWave);
        }
        OnNewWave?.Invoke(waveCount, timeBetweenWaves);
        yield return new WaitForSeconds(timeBetweenWaves);
        StartWave();
    }
    
     

    void StartWave()
    {
        //PREPEI NA AUKSANEI TO DIFFICULTY
        //(DEN EINAI ANAGKH DIFFICULTY = PARAPANW ZWH, ----> PAROLAYTA PREPEI NA AUKSISW TIN ARXIKH ZWH GIA BALANCE
        //THA TO KANW WSTE DIFFICULTY PERISSOTEROI EXTHROI KAI DIAFOTERIKOY TYPOU)
        waveCount++;
        poolSize += 5; //DIFFICULTY ++
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
