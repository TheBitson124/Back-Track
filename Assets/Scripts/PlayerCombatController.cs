using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCombatController : MonoBehaviour
{
    MeleeCollider _meleeCollider;
    void Start()
    {
        _meleeCollider = GetComponentInChildren<MeleeCollider>();
    }
    void Update()
    {
        //print(_meleeCollider.GetEnemies().Count);
    }
}
