using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    public delegate void EnemyDestroyedHandler();
    public event EnemyDestroyedHandler OnEnemyDestroyed;

    void OnDestroy()
    {
        // Trigger the event when the enemy is destroyed
        if (OnEnemyDestroyed != null)
        {
            OnEnemyDestroyed.Invoke();
        }
    }
}