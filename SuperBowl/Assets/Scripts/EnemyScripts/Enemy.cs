using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
     public int pointValue = 10;

    public delegate void EnemyDestroyedHandler();
    public event EnemyDestroyedHandler OnEnemyDestroyed;

    void OnDestroy()
    {
        ScoreManager.Instance.AddPoints(pointValue); // Award points

        // Trigger the event when the enemy is destroyed
        if (OnEnemyDestroyed != null)
        {
            OnEnemyDestroyed.Invoke();
        }
    }
}