using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerSpawns : MonoBehaviour
{
    [SerializeField] List<Transform> spawns;
    int _currentSpawn;
    void Start()
    {
        _currentSpawn = 0;
    }
    public Transform getNextSpawn()
    {
        _currentSpawn++;
        return spawns[_currentSpawn];
    }
    public Transform getSpawn(int round)
    {
        return spawns[_currentSpawn];
    }
}
