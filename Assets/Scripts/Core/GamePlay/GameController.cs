using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;

public class GameController : SingleTon<GameController>
{
    public static event Action<int> waveInfoEvent;
    public int totalWaves = 3;
    public int currentWave = 0;
    public Enemy[] enemys;
    public int curentEnenemycount = 0;
    public int killCount = 0;
    public PlayerController player;
    public int playerSpawnCount = 3;
    void Start()
    {
        StartCoroutine(StartNextWave());
    }
    private IEnumerator StartNextWave()
    {
        currentWave++;
        waveInfoEvent?.Invoke(currentWave);

        yield return new WaitForSeconds(2f);
        for (int i = 1; i <= currentWave; i++)
        {
            enemys[i - 1].Spawn();
            curentEnenemycount++;
        }
        InputManager.Instance.lockInputControll = false;
    }
    public void UpdateEnemyCount()
    {
        curentEnenemycount--;
        if (curentEnenemycount <= 0)
        {
            if (currentWave == totalWaves)
            {
                //game over
                return;
            }
            UIController.Instance.UpdateKillInfo(killCount++);
            InputManager.Instance.lockInputControll = true;
            CameraFollow.Instance.ResetCamera();
            player.RestPosition();
            //trigger next wave
            StartCoroutine(StartNextWave());
        }
    }
    Coroutine playerspawnRoutine;
    internal void RespawnPlayer()
    {
        playerspawnRoutine = StartCoroutine(SpawnPlayer());

    }

    private IEnumerator SpawnPlayer()
    {
        playerSpawnCount--;
        yield return new WaitForSeconds(1);
        if (playerSpawnCount > 0)
        {
            player.Spawn();
            InputManager.Instance.lockInputControll = false;
            CameraFollow.Instance.ResetCamera();
            UIController.Instance.UpdatePlayerHealth(100, 100);
        }
        StopCoroutine(playerspawnRoutine);
    }
}
