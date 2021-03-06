using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLocator : MonoBehaviour
{
    [SerializeField] Transform weapon;
    Transform target;

    [SerializeField] float towerRange = 15f;
    [SerializeField] ParticleSystem arrow;
    

    void Update()
    {
        FindClosestTarget();
        AimWeapon();
    }

    
    void FindClosestTarget()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        Transform closestTarget = null;
        float maxDistance = Mathf.Infinity;

        foreach (Enemy enemy in enemies)
        {
            float targetDistance = Vector3.Distance(transform.position, enemy.transform.position);

            if (targetDistance < maxDistance)
            {
                closestTarget = enemy.transform;
                maxDistance = targetDistance;
            }
        }

        target = closestTarget;

    }
    
    
    void AimWeapon()
    {
        if (target == null)
        {
            return;
        }
        
        float targetDistance = Vector3.Distance(transform.position, target.position);
        
        weapon.LookAt(target);

        if (targetDistance < towerRange)
        {
            Attack(true);
        }
        else
        {
            Attack(false);
        }
    }

    void Attack(bool isActive)
    {
        var emissionModule = arrow.emission;
        emissionModule.enabled = isActive;
    }
}
