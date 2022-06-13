using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] GameObject[] enemyprefab;
    [SerializeField][Range(0.1f, 30f)] float spawnTimer = 1f;
    [SerializeField][Range(0,50)] int poolSize = 5;
    
    [SerializeField] [Range(1f, 10f)] float timeBetweenWaves = 1f; 
    

    List<GameObject> tempPool = new List<GameObject>();

    int waveCount = 1;
    
    int activeInSceneCount;
 
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
            StartCoroutine(PrepareForWave());
        }
    }

   
    void Start()
    {
        StartCoroutine(SpawnEnemy());
        activeInSceneCount = poolSize;
    }

 
    void PopulatePool( int tempPoolSize)
    {
        for (int i = 0; i < tempPoolSize; i++)
        {
            var tempEnemy= Instantiate(enemyprefab[0], transform);
            tempEnemy.SetActive(false);
            tempPool.Add(tempEnemy);
        }
    }


    IEnumerator SpawnEnemy()
    {
      
        for (int i = 0; i < tempPool.Count; i++)
        {
            tempPool[i].SetActive(true);
            yield return new WaitForSeconds(spawnTimer);
        }
    }

   
    IEnumerator PrepareForWave()
    {
        //gameobject 3,2,1 setActive(true);
        yield return new WaitForSeconds(timeBetweenWaves);
        StartWave();
    }
    
     

    void StartWave()
    {
        //gameobject 3,2,1 setActive(false);
        waveCount++;
        poolSize += 5;
        tempPool.Clear();
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
