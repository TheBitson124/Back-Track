using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObjects/Test")]
public class EnemyStats : ScriptableObject
{
    [SerializeField]
    int maxHp;
    int hp;
    public int attackDamage;
    private void Awake()
    {
        hp = maxHp;
    }
    public bool TakeDamage(int damage)
    {
        hp -= damage;
        if (hp < 0)
        {
            Debug.Log("Dead");
        }
        return false;
    }
}
