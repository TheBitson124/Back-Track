using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    int _round;
    int round
    {
        get => _round;  
        set{
            _round = value;
            OnRoundChanged?.Invoke(_round);
        }
    }
    public static event Action<int> OnRoundChanged;
    public GameObject Player;
    public PlayerSpawns playerSpawns;
    private Transform nextSpawn;
    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        Player.transform.position = playerSpawns.getSpawn(0).position;
        Player.transform.rotation = playerSpawns.getSpawn(0).rotation;

    }
    public void nextRound()
    {
        round++;
        Player.transform.position = playerSpawns.getNextSpawn().position;

    }
    void Update()
    {
        
    }
}
