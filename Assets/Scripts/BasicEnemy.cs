using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    public EnemyStats enemyStats;
    public Transform Player;
    public LayerMask WallMask;
    //public float chaseSpeed;
    //public float stoppingDistance;

    public float sightRange;
    public float attackRange;
    public float fov;
    public float rotationSpeed;
    public float chaseSpeed;
    private bool canSeePlayer = false;
    public float attackCooldown;

    public bool isAttacking = false;
    
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (Player == null)
        {
            Debug.Log("no player");
            return;
        }
        if (isAttacking) return;
        CheckVision();
        if (canSeePlayer)
        {
            ChasePlayer();
        }
    }
    void CheckVision()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, Player.position);
        if (distanceToPlayer < sightRange)
        {
            Vector3 directionToPlayer = (Player.position - transform.position).normalized;
            float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);
            if (angleToPlayer <= fov * 0.5)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, directionToPlayer, out hit, sightRange,WallMask))
                {
                    Transform hitTransform = hit.transform;
                    while (hitTransform != null)
                    {
                        if (hitTransform.CompareTag("Player"))
                        {
                            canSeePlayer = true;
                            return;
                        }
                        hitTransform = hitTransform.parent;
                    }
                    
                }
                else
                {
                    //Debug.Log(directionToPlayer);
                }
            }
            else
            {
               // Debug.Log("Player OUT OF FOV");

            }
        }
        else
        {
           // Debug.Log("Player too far");
        }
    }
    void ChasePlayer()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, Player.position);

        if(distanceToPlayer < attackRange)
        {
            isAttacking = true;
            StartCoroutine(AttackCooldown(attackCooldown));
            Attack();
            return; 
        }
        else
        {
            Vector3 direction = (Player.position - transform.position).normalized;
            direction.y = 0;
            if (direction != Vector3.zero)
            {
                //Debug.Log("Rotate");
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
            transform.Translate(Vector3.forward * chaseSpeed * Time.deltaTime);
        }
    }
    IEnumerator AttackCooldown(float time)
    {
        yield return new WaitForSeconds(time);
        isAttacking = false;

    }
    void Attack()
    {
        GameObject Player = GameObject.FindGameObjectWithTag("Player");
        Player.GetComponent<PlayerCombatController>().TakeDamage(enemyStats.attackDamage);
        Debug.Log("Attacking Player");
    }
}
