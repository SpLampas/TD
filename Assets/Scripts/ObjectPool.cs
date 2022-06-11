using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] GameObject[] enemyprefab;
    [SerializeField][Range(0.1f, 30f)] float spawnTimer = 1f;
    [SerializeField][Range(0,50)] int poolSize = 5;
    // [SerializeField] [Range(1f, 3f)] float timeBetweenWaves = 1f; 
    float countDown;//time until first wave starts
    
    GameObject[] pool;

    
    
    void Awake()
    {
        PopulatePool();
    }

    
    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    
    
    void PopulatePool()
    {
        pool = new GameObject[poolSize];
        for (int i = 0; i < pool.Length; i++)
        {
            pool[i] = Instantiate(enemyprefab[0], transform);
            pool[i].SetActive(false);
        }
    }
    
    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            EnableObjectInPool();
            yield return new WaitForSeconds(spawnTimer);
        }
    }

    //prepei na allaxtoun h panw kai h katw
    
    void EnableObjectInPool()
    {
        for (int i = 0; i < pool.Length; i++)
        {
            if (pool[i].activeInHierarchy == false)
            {
                pool[i].SetActive(true);
                return;
            }
        }
    }
    
    
  
}
