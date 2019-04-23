﻿using UnityEngine;

public class Board : MonoBehaviour
{
    public BoardStateVariable state;
    public Transform playerSpawn;
    public Transform enemySpawn;

    private void Start()
    {
        state.currentState.playerSpawnPosition = playerSpawn;
        state.currentState.npcSpawnPosition = enemySpawn;
    }
}