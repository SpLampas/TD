using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] List<Waypoint> path = new List<Waypoint>();
    [SerializeField][Range(0f,5f)] float speed = 1f;

    Enemy enemy;
    
    
    private void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    void OnEnable()
    { 
        FindPath();
        ReturnToStart();
        StartCoroutine(FollowPath());   
    }

    void FindPath()
    {
        path.Clear();
        
        GameObject parent = GameObject.FindGameObjectWithTag("Path");

        foreach (Transform child in parent.transform)
        {
            Waypoint waypoint = child.GetComponent<Waypoint>();
            if (waypoint != null)
            {
                path.Add(waypoint);  
            }
            
        }
    }

    void ReturnToStart()
    {
        transform.position = path[0].transform.position;
    }
    
    
    IEnumerator FollowPath()
    {
        foreach (var waypoint in path)
        {
            Vector3 startPosition = transform.position;
            Vector3 endPosition = waypoint.transform.position;
            float travelPercentage = 0;
            
            transform.LookAt(endPosition);

            while (travelPercentage < 1f)
            {
                travelPercentage += Time.deltaTime*speed;
                transform.position = Vector3.Lerp(startPosition, endPosition, travelPercentage);
                
                yield return new WaitForEndOfFrame();
            }
        }
        
        FinishPath();
        
        
    }
    
    void FinishPath()
    {
        enemy.PenaltyGold();
        var deadEnemy = 0;
        gameObject.SetActive(false);
        Actions.OnDeath(deadEnemy);
        gameObject.SetActive(false);
    }
}
