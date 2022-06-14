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
    // [Tooltip("Adds amount to maxHitPoints when Enemy dies.")]
    // [SerializeField] int difficulty = 1;

    [SerializeField,ReadOnly] ParticleSystem coinParticle;
    
    [SerializeField] int currentHitPoints;

    [SerializeField] Image healthBar;
    
    Enemy enemy;
    public static Action<int> OnDeath;
    
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
            // coinParticle.gameObject.SetActive(true);
            // coinParticle.Play();
            // coinParticle.transform.SetParent(null,true);
            
            gameObject.SetActive(false);

            var deadEnemy = -1;
            OnDeath?.Invoke(deadEnemy);
            
            // maxHitPoints += difficulty; //??? μαλλον πρεπει να γινεται αλλου gia na exei sxesh me to wave
            enemy.RewardGold(); 
        }
    }
    
}
