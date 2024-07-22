using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoints : MonoBehaviour
{
    [SerializeField] Difficulty theDifficulty;
    public GameObject theSpawn;
    public List<GameObject> theSpawners;

    // Variables from difficulty script
    private int amountToSpawn;
    public int currentWave;
    public int totalWave;
    public int maxActive;
    public int minActive;
    public float waveTimer;
    public float spawnDelay;

    // Getting spawning referenced from camera bounds
    public GameObject theCamera;
    private cameraMovement bounds;
    public Vector2 spawnRandom;

    public float currentWaveTimer;
    public bool waveActive;

    void Start()
    {
        amountToSpawn = theDifficulty.numberOfSpawners;
        totalWave = theDifficulty.numberOfWaves;
        waveTimer = theDifficulty.waveTimer;
        maxActive = theDifficulty.maxActiveSpawners;
        minActive = theDifficulty.minActiveSpawners;

        currentWave = 1;
        currentWaveTimer = waveTimer;

        for (int i = 0; i < amountToSpawn; i++)
        {
            theSpawners.Add(theSpawn);
        }

        waveActive = true;
        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        while (waveActive && currentWave <= totalWave)
        {
            // Wait for the current wave timer
            yield return new WaitForSeconds(currentWaveTimer);

            // Determine how many spawners to spawn for this wave
            int theWavePointsToSpawn = Random.Range(minActive, maxActive + 1);

            for (int i = 0; i < theWavePointsToSpawn; i++)
            {
                SpawnRandomPoints();
                yield return new WaitForSeconds(spawnDelay); // Optional delay between each spawner spawn
            }

            currentWave++;
        }

        waveActive = false; // Disable wave spawning after all waves are spawned
    }

    void SpawnRandomPoints()
    {
        int randomIndex = Random.Range(0, amountToSpawn); // Random index within theSpawners list
        GetRandomPoint(); // Get a random spawn point
        Instantiate(theSpawners[randomIndex], spawnRandom, Quaternion.identity); // Instantiate the spawner at the random point
    }

    void GetRandomPoint()
    {
        bounds = theCamera.GetComponent<cameraMovement>();

        float randomX = Random.Range(bounds.min.x, bounds.max.x);
        float randomY = Random.Range(bounds.min.y, bounds.max.y);

        spawnRandom = new Vector2 (randomX, randomY);
    }

    void Update()
    {
        // Optional: Check current state, debug info, etc.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log($"Wave {currentWave} Timer: {currentWaveTimer}");
        }
    }
}