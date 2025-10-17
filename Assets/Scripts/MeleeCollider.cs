using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MeleeCollider : MonoBehaviour
{
    List<BasicEnemy> enemies;
    public Transform PlayerOrientation;
    public List<BasicEnemy> GetEnemies() { return enemies; }
    void Start()
    {
        enemies = new List<BasicEnemy>();
    }
    private void Update()
    {
        transform.position = PlayerOrientation.position + PlayerOrientation.forward;
        Collider[] hitColliders = Physics.OverlapBox(transform.position, Vector3.Scale(GetComponent<BoxCollider>().size * 0.5f, transform.lossyScale), PlayerOrientation.rotation);
        Debug.Log(hitColliders);

    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Trigger entered with: {other.name} on layer {other.gameObject.layer}");
        if (other.TryGetComponent<BasicEnemy>(out BasicEnemy enemy))
        {
            if (!enemies.Contains(enemy))
            {
                enemies.Add(enemy);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<BasicEnemy>(out BasicEnemy enemy))
        {
            if (enemies.Contains(enemy))
            {
                enemies.Remove(enemy);
            }
        }
       
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (GetComponent<BoxCollider>())
        {
            BoxCollider box = GetComponent<BoxCollider>();
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.DrawWireCube(box.center, box.size);
        }
        else if (GetComponent<SphereCollider>())
        {
            SphereCollider sphere = GetComponent<SphereCollider>();
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.DrawWireSphere(sphere.center, sphere.radius);
        }
    }
}
