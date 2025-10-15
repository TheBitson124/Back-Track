using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeCollider : MonoBehaviour
{
    List<BasicEnemy> enemies;
    public List<BasicEnemy> GetEnemies() { return enemies; }
    void Start()
    {
        enemies = new List<BasicEnemy>();
    }
    private void OnTriggerEnter(Collider other)
    {
        print(other.name);
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

}
