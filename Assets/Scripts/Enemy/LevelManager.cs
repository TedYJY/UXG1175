using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Written by: Ryan Jacob && Tedmund Yap
public class LevelManager : MonoBehaviour
{
    [SerializeField] Difficulty theDifficulty;
    public GameObject theSpawn;
    public List<GameObject> theSpawners;

    [SerializeField]
    private GameObject enemySpawnHandler;

    public Difficulty[] difficultyList;

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

    public int enemiesRemaining;

    public GameObject lastWave;
    public GameObject completion;

    void Start()
    {
        int selectedDifficulryIndex = CharacterSelectionManager.SelectedDifficultyIndex ; //getting selected difficulty index from selection manager

        theDifficulty = difficultyList[selectedDifficulryIndex];


        // assignning from scriptableObj
        amountToSpawn = theDifficulty.numberOfSpawners;
        totalWave = theDifficulty.numberOfWaves;
        waveTimer = theDifficulty.waveTimer;
        maxActive = theDifficulty.maxActiveSpawners;
        minActive = theDifficulty.minActiveSpawners;

        currentWave = 0;
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
        while (waveActive && currentWave < totalWave)
        {
            // wait for the current wave timer
            yield return new WaitForSeconds(currentWaveTimer);

           
            int theWavePointsToSpawn = Random.Range(minActive, maxActive + 1);  // determine how many spawners to spawn for this wave

            for (int i = 0; i < theWavePointsToSpawn; i++)
            {
                SpawnRandomPoints();
                yield return new WaitForSeconds(spawnDelay); // wait for timer to run out
                

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
        elapsedTime += Time.deltaTime; // elasped time in game
        totalElapsedTime = (int)elapsedTime; //timer to keep track of play time


        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log($"Wave {currentWave} Timer: {currentWaveTimer}");
        }

        if (thePlayer != null) 
        
        {
            savedTime = totalElapsedTime;
        }

        enemiesRemaining = GameObject.FindGameObjectsWithTag("Enemy").Length;// detect how many enemies in the scene



        if (currentWave >= totalWave && enemiesRemaining == 0 ) //end game condition
        {
            completion.SetActive(true);
            lastWave.SetActive(false);
            StartCoroutine(waitTimer());
            SceneManager.LoadScene("EndGame");
        }

        if (currentWave == totalWave -1)
        {
            lastWave.SetActive(true);
            foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                enemy.GetComponent<Enemy>().foundPlayer = true;
            }
        }

    }

    IEnumerator waitTimer()
    {
        yield return new WaitForSeconds(5f);
    }

    

    

    
}