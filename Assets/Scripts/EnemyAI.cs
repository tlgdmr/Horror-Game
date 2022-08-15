using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;

public class EnemyAI : MonoBehaviour
{
    Transform TargetToLookAt;
    Transform Player;
    [SerializeField] float chaseRange;

    [SerializeField] LayerMask Ground;

    NavMeshAgent navMeshAgent;

    Vector3 walkPoint;
    
    bool isHit = false;
    bool walkPointSet = false;
    bool isChasing = false;
    bool dead = false;
    
    float distance;
    [SerializeField] float turnSpeed;
    [SerializeField] float walkPointRange;

    int health = 100;
    int damage = 20;
    [SerializeField] int endChasingTime;

    EnemySpawn enemySpawn;
    ScoreManager scoreManager;
    PlayerHealth playerHealth;
    void Start()
    {
        TargetToLookAt = FindObjectOfType<PlayerMovement>().transform.GetChild(3);
        Player = FindObjectOfType<PlayerMovement>().transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
        enemySpawn = FindObjectOfType<EnemySpawn>();
        scoreManager = FindObjectOfType<ScoreManager>();
        playerHealth = FindObjectOfType<PlayerHealth>();
    }

    void Update()
    {
        CheckDistance();
    }

    private void OnParticleCollision(GameObject other)
    {
        IsHit();
    }

    public void EnemyHealth(int damage)
    {
        health -= damage;
        GetComponent<Animator>().SetTrigger("Damage");
        if (health <= 0 && !dead)
        {
            GetComponent<Animator>().SetTrigger("Dead");
            navMeshAgent.enabled = false;
            Destroy(gameObject, 10);
            dead = true;
            scoreManager.UpdateScore();
            enemySpawn.numberOfEnemies--;
            this.GetComponent<EnemyAI>().enabled = false;
        }
    }

    public void IsHit()
    {
        isHit = true;
        EnemyHealth(damage);
    }

    private void CheckDistance()
    {
        distance = Vector3.Distance(Player.position, transform.position);

        if (isHit)
        {
            walkPointSet = false;
            EngageTarget();
        }
        else if (distance <= chaseRange)
        {
            walkPointSet = false;
            EngageTarget();
        }
        else if (distance > chaseRange && isChasing)
        {
            Invoke(nameof(Patrolling), endChasingTime);
        }
        else 
        {
            Patrolling();
        }
    }

    void EngageTarget()
    {
        FaceToTarget();
        if (distance <= navMeshAgent.stoppingDistance + 1)
        {
            GetComponent<Animator>().SetBool("Chasing", false);
            AttackTarget();
        }
        else 
        {
            ChaseTarget();
        }
    }

    private void AttackTarget()
    {
        GetComponent<Animator>().SetTrigger("Attacking");
    }

    void FaceToTarget()
    {
        if (walkPointSet)
        {
            Vector3 directionToWalkPoint = (walkPoint - transform.position).normalized;
            Quaternion lookRotationForWalkPoint = Quaternion.LookRotation(new Vector3(directionToWalkPoint.x, 0, directionToWalkPoint.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotationForWalkPoint, turnSpeed * Time.deltaTime);
        }
        else
        {
            Vector3 direction = (TargetToLookAt.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, turnSpeed * Time.deltaTime);
        }
    }
    void Patrolling()
    {
        if (isChasing)
        {
            isChasing = false;
            CancelInvoke(nameof(Patrolling));
        }

        if (!walkPointSet)
        {
            GetComponent<Animator>().SetBool("Idle", true);
            SetWalkPoint();
            return;
        }
        else
        {
            FaceToTarget();
            GetComponent<Animator>().SetBool("Idle", false);
            GetComponent<Animator>().SetBool("Chasing", false);
            GetComponent<Animator>().SetBool("Patrolling", true);
            navMeshAgent.speed = 4;
            navMeshAgent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 5)
        {
            walkPointSet = false;
        }
    }

    private void SetWalkPoint()
    {
        float randomXValue = UnityEngine.Random.Range(-walkPointRange, walkPointRange);
        float randomZValue = UnityEngine.Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomXValue, transform.position.y, transform.position.z + randomZValue);

        if (Physics.Raycast(walkPoint, -transform.up, 2, Ground))
        {
            walkPointSet = true;
        }
    }

    private void ChaseTarget()
    {
        GetComponent<Animator>().SetBool("Patrolling", false);
        GetComponent<Animator>().SetBool("Chasing", true);
        navMeshAgent.speed = 6;
        navMeshAgent.SetDestination(TargetToLookAt.position);
        isChasing = true;
    }

    void DecreasePlayerHealth()
    {
        playerHealth.DecreasePlayerHealth();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
}
