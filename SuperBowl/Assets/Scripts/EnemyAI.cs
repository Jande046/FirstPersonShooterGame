using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform target;
    private EnemyReferences enemyReferences;
    private float pathUpdateDeadline;
    private float attackDistance;

     [Header("Damage Settings")]
    public float damageAmount = 10f;     // Damage dealt by this enemy
    public float attackCooldown = 1.5f; // Time between attacks

    private bool isAttacking = false;   // Tracks whether the enemy is currently attacking
    private float nextAttackTime = 0f;  // Time when the enemy can attack again
    

    private void Awake(){
        enemyReferences = GetComponent<EnemyReferences>();
    }

    void Start(){
         // Dynamically assign the player as the target
        if (target == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                target = player.transform;
            }
            else
            {
                Debug.LogError("Player object not found in the scene! Ensure your player has the 'Player' tag.");
            }
        }
        attackDistance = enemyReferences.navMeshAgent.stoppingDistance;
    }

    void Update(){
        if(target != null){
            float distanceToTarget = Vector3.Distance(transform.position, target.position);
            bool inRange = distanceToTarget <= attackDistance;
            if(inRange){
                LookAtTarget();
                  // Handle attack logic
                if (Time.time >= nextAttackTime)
                {
                    Attack();
                }
            } else{
                UpdatePath();
            }
        }
    }
    
    private void LookAtTarget(){
        Vector3 lookPos = target.position - transform.position;
        lookPos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.2f);

    }

    private void UpdatePath(){
        if(Time.time >= pathUpdateDeadline){
            //Debug.Log("Updating Path");
            pathUpdateDeadline = Time.time + enemyReferences.pathUpdateDelay;
            enemyReferences.navMeshAgent.SetDestination(target.position);
        }
    }

    private void Attack()
    {
        if (isAttacking) return;

        isAttacking = true;

        // Trigger attack animation (if applicable)
        Debug.Log($"{gameObject.name} is attacking!");

        // Apply damage to the player
        PlayerDamage playerDamage = target.GetComponent<PlayerDamage>();
        if (playerDamage != null)
        {
            PlayerHealth playerHealth = target.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount);
            }
        }

        // Set cooldown for the next attack
        nextAttackTime = Time.time + attackCooldown;

        // End attack after a brief delay
        Invoke(nameof(ResetAttack), 0.5f); // Adjust delay based on your attack animation
    }

    private void ResetAttack()
    {
        isAttacking = false;
    }
}
