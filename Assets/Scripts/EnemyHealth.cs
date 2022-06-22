using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Enemy))]
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHitPoints = 5;
    
    [SerializeField,ReadOnly] ParticleSystem coinParticle;
    
    [SerializeField] int currentHitPoints;

    [SerializeField] Image healthBar;
    
    Enemy enemy;

    void Start()
    {
        enemy = GetComponent<Enemy>();
        coinParticle = GetComponentInChildren<ParticleSystem>();
    }

    void OnEnable()
    {
        currentHitPoints = maxHitPoints; ;
        healthBar.fillAmount = 1f;
    }

    void OnParticleCollision(GameObject other)
    {
        ProcessHit();
    }

    void ProcessHit()
    {
        currentHitPoints--;
        healthBar.fillAmount = (float)currentHitPoints/maxHitPoints;

        if (currentHitPoints == 0)
        {
            coinParticle.gameObject.SetActive(true);
            coinParticle.Play();
            coinParticle.transform.SetParent(null,true);
            Destroy(coinParticle.gameObject,2f);
            
            gameObject.SetActive(false);

            var deadEnemy = 1;
            Actions.OnDeath(deadEnemy);
            // maxHitPoints += difficulty; //??? μαλλον πρεπει να γινεται αλλου gia na exei sxesh me to wave
            enemy.RewardGold(); 
        }
    }
    
}
