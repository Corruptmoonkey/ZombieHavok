// Written by Jay Gunderson
// 10/15/2024
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class ZombieSpawnController : MonoBehaviour
{
    public int initialZombiesPerWave = 10;
    public int currentZombiesPerWave;

    public float spawnDelay = 2f; // Delay between each zombie spawn

    public static int currentWave = 0;
    public float waveCooldown = 10.0f; // Time between when the wave ends and the new wave starts

    public bool inCooldown;

    public float cooldownCounter = 0;

    public List<Enemy> currentZombiesAlive;
    public GameObject zombiePrefab;

    public GameObject bossPrefab; // Boss prefab
    public Transform bossSpawnPoint; // Reference to the boss spawn point
    private Enemy bossEnemy; // Reference to the boss enemy

    public TextMeshProUGUI waveOverUI;
    public TextMeshProUGUI cooldownCounterUI;
    public TextMeshProUGUI currentWaveUI;

    public List<Transform> spawnPoints;
    public TextMeshProUGUI roundOverUI;

    public SpawnDrop SpawnDrop;

    private void Start()
    {
        currentWave = 0;
        currentZombiesPerWave = initialZombiesPerWave;
        StartNextWave();
    }

    private void StartNextWave()
    {
        SpawnDrop.SpawnRandom();
        if (currentWave < 10)
        {
            currentZombiesAlive.Clear();
            bossEnemy = null; // Reset boss reference at the start of the new wave

            currentWave++;
            currentWaveUI.text = "Wave:  " + currentWave.ToString();

            StartCoroutine(SpawnWave());
        }

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

        // Check if the current wave is 10 and spawn the boss
        // Bad coding practices, but I needed a way to increase the amount of bosses every 5 rounds 
        if (currentWave >= 10 && currentWave < 15)
        {
            SpawnBoss();

        }
        else if(currentWave >= 15 && currentWave < 20)
        {
            SpawnBoss();
            SpawnBoss();
        }
        else if (currentWave >= 20 && currentWave < 25)
        {
            SpawnBoss();
            SpawnBoss(); SpawnBoss();
        }
        else if (currentWave >= 25 && currentWave < 30)
        {
            SpawnBoss(); SpawnBoss();
            SpawnBoss(); SpawnBoss();
        }

        else if (currentWave >= 30 )
        {
            SpawnBoss(); SpawnBoss(); SpawnBoss();
            SpawnBoss(); SpawnBoss();
        }
    }

    private void SpawnBoss()
    {
        if (bossSpawnPoint != null && bossPrefab != null)
        {
            // Instantiate the boss at the boss spawn point
            var boss = Instantiate(bossPrefab, bossSpawnPoint.position, Quaternion.identity);
            bossEnemy = boss.GetComponent<Enemy>(); // Track the boss
            Debug.Log("Boss spawned!");
        }
        else
        {
            Debug.LogError("Boss spawn point or boss prefab is not set!");
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

        // Start Cooldown if all zombies and the boss are dead
        if (currentZombiesAlive.Count == 0 && (bossEnemy == null || bossEnemy.isDead) && !inCooldown)
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
            cooldownCounter = waveCooldown + 1.5f;
        }

        cooldownCounterUI.text = cooldownCounter.ToString("F0");
    }

    private IEnumerator WaveCooldown()
    {
        inCooldown = true;
        yield return new WaitForSeconds(1.5f);
             
            waveOverUI.gameObject.SetActive(true);

            yield return new WaitForSeconds(waveCooldown);
            inCooldown = false;
            waveOverUI.gameObject.SetActive(false);

            currentZombiesPerWave += 5; // Increase the number of zombies for the next wave
            StartNextWave();
        
     
    }


}