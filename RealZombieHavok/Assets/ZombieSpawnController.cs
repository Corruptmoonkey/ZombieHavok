// Written by Jay Gunderson
// 10/15/2024
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class ZombieSpawnController : MonoBehaviour
{
    public int initialZombiesPerWave = 5;
    public int currentZombiesPerWave;

    public float spawnDelay = 0.5f; // Delay between each zombie spawn

    public int currentWave = 0;
    public float waveCooldown = 10.0f; // Time between when the wave ends and the new wave starts

    public bool inCooldown;

    public float cooldownCounter = 0;


    public List<Enemy> currentZombiesAlive;
    public GameObject zombiePrefab;

    public TextMeshProUGUI waveOverUI;
    public TextMeshProUGUI cooldownCounterUI;
    public TextMeshProUGUI currentWaveUI;


    public List<Transform> spawnPoints;

    private void Start()
    {
        currentZombiesPerWave = initialZombiesPerWave;
       // GlobalReferences.Instance.waveNumber = currentWave;
        StartNextWave();

    }

    private void StartNextWave()
    {
        currentZombiesAlive.Clear();

        currentWave++;
       // GlobalReferences.Instance.waveNumber = currentWave;
        currentWaveUI.text = "Wave:  " + currentWave.ToString();

        StartCoroutine(SpawnWave());


    }

    private IEnumerator SpawnWave()
    {
        for (int i = 0; i < currentZombiesPerWave; i++)
        {
            // Check if there are spawn points defined
            if (spawnPoints.Count == 0)
            {
                Debug.LogError("No spawn points defined!");
                yield break;
            }

            // Pick a random spawn point
            Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];

            // Generate random offset within a specified range around the spawn point (optional)
            Vector3 spawnOffset = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f));
            Vector3 spawnPosition = randomSpawnPoint.position + spawnOffset;

            // Instantiate the zombie at the random spawn position
            var zombie = Instantiate(zombiePrefab, spawnPosition, Quaternion.identity);

            // Get Enemy Script
            Enemy enemyScript = zombie.GetComponent<Enemy>();

            // Track this zombie
            currentZombiesAlive.Add(enemyScript);

            yield return new WaitForSeconds(spawnDelay); // Delay between spawns
        }
    }

    private void Update()
    {
        // Get all dead zombies
        List<Enemy> zombiesToRemove = new List<Enemy>();
        foreach (Enemy zombie in currentZombiesAlive) // Adds dead zombies to a list
        {
            if (zombie.isDead)
            {
                zombiesToRemove.Add(zombie);
            }
        }

        // Removes each dead zombie
        foreach (Enemy zombie in zombiesToRemove)
        {
            currentZombiesAlive.Remove(zombie); // Removes them from the game 
        }

        zombiesToRemove.Clear();

        // Start Cooldown if all zombies are dead
        if (currentZombiesAlive.Count == 0 && inCooldown == false)
        {
            StartCoroutine(WaveCooldown());
        }

        // Run the cooldown counter
        if (inCooldown)
        {
            cooldownCounter -= Time.deltaTime;
        }
        else
        {
            cooldownCounter = waveCooldown;
        }

        cooldownCounterUI.text = cooldownCounter.ToString("F0");
    }

    private IEnumerator WaveCooldown()
    {
        inCooldown = true;

        waveOverUI.gameObject.SetActive(true);

        yield return new WaitForSeconds(waveCooldown);
        inCooldown = false;
        waveOverUI.gameObject.SetActive(false);

        currentZombiesPerWave *= 2;
        StartNextWave();
    }
}