using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

//Written by: Tedmund Yap
public class EnemyWaveHandler : MonoBehaviour
{
    //References to enemyWaves CSV to get data
    private static string CSVpath = "/CSVs/enemyWaves.csv";

    [SerializeField]
    private GameObject spawnHandler;
    private EnemySpawner spawner;
    public int lvl;

    private string[][] currentlvl;
    private List<string[]> lvl1 = new List<string[]>();
    private List<string[]> lvl2 = new List<string[]>();

    private string[] splitLines;
    private string[] splitEnemies;


    void Awake()
    {
        switch (SceneManager.GetActiveScene().buildIndex)
        {
            case 2:
                lvl = 1;
                break;

            case 3:
                lvl = 2;
                break;

            default:
                break;
        }

        string[] allLines = File.ReadAllLines(Application.dataPath + CSVpath);

        //Loops through the String of allLines, skipping past first and last line (First line is the headers, bottom line CSV leaves a blank line by default)
        for (int i = 1; i < allLines.Length; i++)
        {

            //Seperates into new Strings using the comma (',') delimiter
            splitLines = allLines[i].Split(new char[] { ',' });

            //Checks if all data entries are filled
            if (splitLines.Length != 4)
            {
                Debug.Log(allLines[i] + " does not have all 4 values!");

                //Returns back if all data entries are not filled for "Mental Stability" purposes
                return;
            }

            //Filters lvl1 and lvl2 waves into new lists
            switch (splitLines[1])
                {
                    case "1":
                        lvl1.Add(splitLines);
                        break;

                    case "2":
                        lvl2.Add(splitLines);
                        break;

                    default:
                        break;
                }

        }
    }

    void Start()
    {
        //Finds spawnHandler and gets the script
        spawnHandler = GameObject.FindWithTag("SpawnHandler");
        spawner = spawnHandler.GetComponent<EnemySpawner>();

        //Starts spawning coroutine when activated
        StartCoroutine(SpawnWithDelay());
    }

    IEnumerator SpawnWithDelay()
    {
        //Switch case checks the current level and chooses which set of waves to choose from
        switch (lvl)
        {
            case 1:
                currentlvl = lvl1.ToArray();
                break;

            case 2:
                currentlvl = lvl2.ToArray();
                break;

            default:
                break;
        }

        //Loop through currentlvl to seperate waves
        for (int i = 0; i < currentlvl.Length; i++)
        {
            //Split currentlvl[i] into an array where each entry contains enemyID#amount
            string[] combinedEnemies = currentlvl[i][2].Split(new char[] { '!' });

            //split splitEnemies into an array where 0 is enemy type and 1 is enemy amount
            for (int y = 0; y < combinedEnemies.Length; y++)
            {
                splitEnemies = combinedEnemies[y].Split(new char[] { '#' });
                //Debug.Log(splitEnemies[0] + splitEnemies[1]);

                for (int x = 0; x < int.Parse(splitEnemies[1]); x++)
                {
                    //Spawns enemies and uses the delay in CSV for each enemy
                    spawner.SpawnEnemy(splitEnemies[0], this.gameObject.transform.position);
                    yield return new WaitForSeconds(float.Parse(currentlvl[i][3]));
                }
            }

        }

        Destroy(this.gameObject);
    }

}
