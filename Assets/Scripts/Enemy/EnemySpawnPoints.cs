using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Written by: Ryan Jacob && Tedmund Yap
public class EnemySpawnPoints : MonoBehaviour
{
    [SerializeField] Difficulty theDifficulty;
    public GameObject theSpawn;
    public List<GameObject> theSpawners;

    [SerializeField]
    private GameObject enemySpawnHandler;

    public GameObject thePlayer;

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

    public int totalElapsedTime;
    public int savedTime;
    private float elapsedTime;


    void Start()
    {
        // assignning from scriptableObj
        amountToSpawn = theDifficulty.numberOfSpawners;
        totalWave = theDifficulty.numberOfWaves;
        waveTimer = theDifficulty.waveTimer;
        maxActive = theDifficulty.maxActiveSpawners;
        minActive = theDifficulty.minActiveSpawners;

        currentWave = 1;
        currentWaveTimer = waveTimer;

        for (int i = 0; i < amountToSpawn; i++) //read csv on how many to spawn and which one to spawn and add it to the list of spawners
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
            // wait for the current wave timer
            yield return new WaitForSeconds(currentWaveTimer);

           
            int theWavePointsToSpawn = Random.Range(minActive, maxActive + 1);  // determine how many spawners to spawn for this wave

            for (int i = 0; i < theWavePointsToSpawn; i++)
            {
                SpawnRandomPoints();
                yield return new WaitForSeconds(spawnDelay); // Optional delay between each spawner spawn
            }

            currentWave++;
        }

        waveActive = false; // disable wave spawning after all waves are spawned
    }

    void SpawnRandomPoints()
    {
        int randomIndex = Random.Range(0, amountToSpawn); // Random index within theSpawners list
        GetRandomPoint(); // Get a random spawn point
        GameObject spawner = Instantiate(theSpawners[randomIndex], spawnRandom, Quaternion.identity); // Instantiate the spawner at the random point
        spawner.SetActive(true);
    }

    void GetRandomPoint()
    {
        bounds = theCamera.GetComponent<cameraMovement>(); // getting the bounds of where to spawn from the camera

        float randomX = Random.Range(bounds.min.x, bounds.max.x);
        float randomY = Random.Range(bounds.min.y, bounds.max.y);

        spawnRandom = new Vector2 (randomX, randomY); //assign new coords of spawn
    }

    void Update()
    {
        elapsedTime += Time.deltaTime; // Accumulate time in float
        totalElapsedTime = (int)elapsedTime; //timer to keep track of play time

        // Optional: Check current state, debug info, etc.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log($"Wave {currentWave} Timer: {currentWaveTimer}");
        }

        if (thePlayer != null) 
        
        {
            savedTime = totalElapsedTime;
        }



    }
}