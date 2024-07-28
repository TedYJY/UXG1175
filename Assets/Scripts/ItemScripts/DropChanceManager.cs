using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;

//Written by: Tedmund Yap
public class DropChanceManager : MonoBehaviour
{
    private static string CSVpath = "/CSVs/ItemDropChance.csv";
    public List<string> DropChanceList = new List<string>();

    public GameObject toSpawn;
    public string temp;

    void Start()
    {
        //Uses ReadAllLines function to seperate CSV based on rows
        //Uses Application.dataPath to ensure that the CSV can be found regardless on project folder location
        string[] allLines = File.ReadAllLines(Application.dataPath + CSVpath);

        //Loops through the String of allLines, skipping past first and last line (First line is the headers, bottom line CSV leaves a blank line by default)
        for (int i = 1; i < allLines.Length; i++)
        {

            //Seperates into new Strings using the comma (',') delimiter
            string[] splitLines = allLines[i].Split(new char[] { ',' });

            //Checks if all data entries are filled
            if (splitLines.Length != 3)
            {
                Debug.Log(allLines[i] + " does not have all 3 values!");

                //Returns back if all data entries are not filled for "Mental Stability" purposes
                return;
            }

            //Passes data from CSV into the SO
            for (int x = 0; x < (float.Parse(splitLines[2]) * 10); x++)
            {
                DropChanceList.Add(splitLines[1]);
            }

        }
    }

    public void DropItem(Vector3 position)
    {
        //Debug.Log("Dropping Item!");
        temp = DropChanceList[Random.Range(0, DropChanceList.Count)];
        toSpawn = Resources.Load<GameObject>(temp);

        Instantiate(toSpawn, position, Quaternion.identity);
    }
}
