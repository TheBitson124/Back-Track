using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObjects/Test")]
public class EnemyStats : ScriptableObject
{
    [SerializeField]
    int maxHp;
    
    int hp;
    private void Awake()
    {
        hp = maxHp;
    }
    public bool TakeDamage(int damage)
    {
        if (hp <= damage)
        {
            hp -= damage;
            return true;
        }
        return false;
    }
}
