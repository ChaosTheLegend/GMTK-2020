﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private SpawnPoint[] SpawnPoints;
    [SerializeField] private Wave[] Waves;
    public UnityEvent onWaveEnd;

    private bool isSpawning = false;
    private bool inProgress = false;
    private int waveId = -1;
    private List<GameObject> SpawnQueue;
    private float tm;


    private void Start()
    {
        StartWave();
    }
    public void StartWave()
    {
        if (Waves.Length - 1 <= waveId) return;
        waveId++;
        SpawnQueue = new List<GameObject>();
        inProgress = true;
        isSpawning = true;
        Wave w = Waves[waveId];
        int i = 0;
        foreach(GameObject gb in w.enemies)
        {
            for(int q = 0;q < w.ammount[i]; q++)
            {
                SpawnQueue.Add(gb);
            }
        }

    }

    private void SpawnEnemy()
    {
        if (SpawnQueue.Count == 0)
        {
            StopSpawn();
            return;
        }
        List<SpawnPoint> availabe = new List<SpawnPoint>();
        foreach(SpawnPoint sp in SpawnPoints)
        {
            if (sp.canSpawn) availabe.Add(sp);
        }
        if (availabe.Count == 0) return;

        int rand = Random.Range(0, availabe.Count);
        int randmob = Random.Range(0, SpawnQueue.Count);

        availabe[rand].Spawn(SpawnQueue[randmob]);
        SpawnQueue.RemoveAt(randmob);
    }

    private void StopSpawn()
    {
        isSpawning = false;
    }
    void Update()
    {

        if (!isSpawning) {
            if (!inProgress) return;
            if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
            {
                onWaveEnd?.Invoke();
                inProgress = false;
            }
            return;
        };
        if(tm > 0)
        {
            tm -= Time.deltaTime;
            return;
        }
        tm = Waves[waveId].spawnDelay;
        int rand = Random.Range(1, Waves[waveId].maxSpawns+1);
        for(int i = 0; i < rand; i++)
        {
            SpawnEnemy();
        }
    }
}
