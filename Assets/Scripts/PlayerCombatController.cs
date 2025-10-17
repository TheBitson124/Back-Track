using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCombatController : MonoBehaviour
{
    public float detectionRange = 3f;
    public float detectionWidth = 2f;
    public float detectionHeight = 2f;
    public bool showDebugGizmos = true;
    private List<BasicEnemy> detectedEnemies = new List<BasicEnemy>();
    private Collider[] detectionBuffer = new Collider[20];

    void Update()
    {
        detectedEnemies.Clear();

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
        print(detectedEnemies.Count);
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
}
