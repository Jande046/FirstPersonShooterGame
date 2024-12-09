using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform target;
    private EnemyReferences enemyReferences;

    private float attackDistance;

    private void Awake(){
        enemyReferences = GetComponent<EnemyReferences>();
    }

    void Start(){
        attackDistance = enemyReferences.navMeshAgent.stoppingDistance;
    }

    void Update(){
        if(target != null){
            bool inRange = Vector3.Distance(transform.position, target.position) <= attackDistance;
            if(inRange){
                LookAtTarget();
            } else{
                UpdatePath();
            }
        }
    }
    
    private void LookAtTarget(){
        Vector3 lookPos = target.position - transform.position;
        lookPos.y = 0;

    }

    private void UpdatePath(){

    }
}
