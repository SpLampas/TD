using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHitPoints = 5;
    [Tooltip("Adds amount to maxHitPoints when Enemy dies.")]
    [SerializeField] int difficulty = 1;

    [SerializeField] GameObject coinParticle;
    
    int currentHitPoints = 0;

    Enemy enemy;


    void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    void OnEnable()
    {
        currentHitPoints = maxHitPoints;
    }

    void OnParticleCollision(GameObject other)
    {
        ProcessHit();
    }

    void ProcessHit()
    {
        currentHitPoints--;
        if (currentHitPoints == 0)
        {
            coinParticle.SetActive(true);
            gameObject.SetActive(false);
            maxHitPoints += difficulty;
            enemy.RewardGold();
        }
    }
}
