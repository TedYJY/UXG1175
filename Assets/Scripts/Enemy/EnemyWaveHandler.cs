using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Rendering;

//Written by: Tedmund Yap
public class EnemyWaveHandler : MonoBehaviour
{
    private static string CSVpath = "/CSVs/enemyWaves.csv";
    [SerializeField]
    private GameObject spawnHandler;
    private EnemySpawner spawner;

    public int lvl = 1;
    private string[][] currentlvl;
    private List<string[]> lvl1 = new List<string[]>();
    private List<string[]> lvl2 = new List<string[]>();

    private string[] splitLines;
    private string[] splitEnemies;


    void Awake()
    {
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

    // Start is called before the first frame update
    void Start()
    {
        spawnHandler = GameObject.FindWithTag("SpawnHandler");
        spawner = spawnHandler.GetComponent<EnemySpawner>();
        StartCoroutine(SpawnWithDelay());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SpawnWithDelay()
    {
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
            //Debug.Log("Starting Loop");
            //Split currentlvl[i] into an array where each entry contains enemyID#amount
            string[] combinedEnemies = currentlvl[i][2].Split(new char[] { '!' });

            //split splitEnemies into an array where 0 is enemy type and 1 is enemy amount
            for (int y = 0; y < combinedEnemies.Length; y++)
            {
                splitEnemies = combinedEnemies[y].Split(new char[] { '#' });
                //Debug.Log(splitEnemies[0] + splitEnemies[1]);

                for (int x = 0; x < int.Parse(splitEnemies[1]); x++)
                {
                    spawner.SpawnEnemy(splitEnemies[0], this.gameObject.transform.position);
                    yield return new WaitForSeconds(0.5f);
                }
            }

        }

        Destroy(this.gameObject);
    }

}
