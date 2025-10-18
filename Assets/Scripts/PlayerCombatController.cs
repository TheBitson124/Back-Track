using scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerCombatController : MonoBehaviour
{
    public float detectionRange = 3f;
    public float detectionWidth = 2f;
    public float detectionHeight = 2f;
    public bool showDebugGizmos = true;
    private List<BasicEnemy> detectedEnemies = new List<BasicEnemy>();
    private Collider[] detectionBuffer = new Collider[20];
    private InputAction attackAction;
    Boolean attackPressed;
    Boolean isAttacking;
    public float attackCooldown = 2f;
    public int attackDamage = 5;
    private void Start()
    {
        isAttacking = false;
        attackAction=gameObject.GetComponent<NewInputPlayerController>().PlayerControls.actions.FindAction("Attack");
        if (attackAction != null)
        {
            attackAction.performed += OnAttackPerformed;
            attackAction.canceled += OnAttackCancelled;
        }
    }
    void Update()
    {
        detectedEnemies.Clear();
        DetectEnemies();
        HandleAttack();
    }
    void HandleAttack()
    {
        if (attackPressed && !isAttacking)
        {
            isAttacking = true;
            Debug.Log("Attack");

            //swing sword
            for (int i = 0; i < detectedEnemies.Count; i++)
            {
                detectedEnemies[i].enemyStats.TakeDamage(attackDamage);
            }
            StartCoroutine(AttackCooldown(attackCooldown));
        }
    }
    void DetectEnemies()
    {
        Vector3 detectionCenter = transform.position + transform.forward * (detectionRange * 0.5f);
        Vector3 halfExtents = new Vector3(detectionWidth * 0.5f, detectionHeight * 0.5f, detectionRange * 0.5f);

        int numColliders = Physics.OverlapBoxNonAlloc(
            detectionCenter,
            halfExtents,
            detectionBuffer,
            transform.rotation
        );
        for (int i = 0; i < numColliders; i++)
        {
            if (detectionBuffer[i].TryGetComponent<BasicEnemy>(out BasicEnemy enemy))
            {
                if (!detectedEnemies.Contains(enemy))
                {
                    detectedEnemies.Add(enemy);
                }
            }
        }
    }
    private void OnDrawGizmos()
    {
        if (!showDebugGizmos) return;

        Gizmos.color = detectedEnemies.Count > 0 ? Color.red : Color.green;
        Gizmos.matrix = Matrix4x4.TRS(
            transform.position + transform.forward * (detectionRange * 0.5f),
            transform.rotation,
            Vector3.one
        );
        Gizmos.DrawWireCube(Vector3.zero, new Vector3(detectionWidth, detectionHeight, detectionRange));

        Gizmos.matrix = Matrix4x4.identity;
        foreach (BasicEnemy enemy in detectedEnemies)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, enemy.transform.position);
        }
    }
    private void OnAttackPerformed(InputAction.CallbackContext context)
    {
        attackPressed = true;
    }
    private void OnAttackCancelled(InputAction.CallbackContext context)
    {
        attackPressed = false;
    }
    IEnumerator AttackCooldown(float time)
    {
        yield return new WaitForSeconds(time);
        isAttacking=false;

    }
}
