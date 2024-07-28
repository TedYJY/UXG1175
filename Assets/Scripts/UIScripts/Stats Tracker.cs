using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;
using UnityEditor.Rendering;

//Written By: Tedmund Yap
public class StatsTracker : MonoBehaviour
{
    private string filename = "";

    public int timesChosenChar1;
    public int timesChosenChar2;
    public int timesChosenMap1;
    public int timesChosenMap2;
    public int totaltimePlayed;
    public int totalDMGtaken;
    public int totalDMGgiven;
    public int totalEnemiesKilled;
    public int meleeEnemiesKilled;
    public int rangedEnemiesKilled;
    public int totalEXPEarned;
    private bool timeSpedUp;

    private void Awake()
    {
        filename = Application.dataPath + "/CSVs/Analytics.csv";

        try
        {
            string[] allLines = File.ReadAllLines(Application.dataPath + "/CSVs/Analytics.csv");

            if (allLines == null)
            {
                Debug.LogError($"CSV file not found at path: {allLines}"); //if csv is not found
                return;
            }

            for (int i = 1; i < allLines.Length; i++) // to read lines from csv
            {
                //Debug.Log(allLines[i]);
                string line = allLines[i];
                string[] splitData = line.Split(','); // splits lines of csv by delimiter

                //Grabbing old data
                timesChosenChar1 = int.Parse(splitData[0]);
                timesChosenChar2 = int.Parse(splitData[1]);
                timesChosenMap1 = int.Parse(splitData[2]);
                timesChosenMap2 = int.Parse(splitData[3]);
                totaltimePlayed = int.Parse(splitData[4]);
                totalDMGtaken = int.Parse(splitData[5]);
                totalDMGgiven = int.Parse(splitData[6]);
                totalEnemiesKilled = int.Parse(splitData[7]);
                meleeEnemiesKilled = int.Parse(splitData[8]);
                rangedEnemiesKilled = int.Parse(splitData[9]);
                totalEXPEarned = int.Parse(splitData[10]);

            }
        }

        catch
        {
            Debug.Log("Old data missing.");
            OverwriteCSV();
        }

    }
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            OverwriteCSV();
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            if (timeSpedUp == false)
            {
                Time.timeScale *= 10;
                timeSpedUp = true;
            }

            else
            {
                Time.timeScale = 1;
                timeSpedUp = false;
            }
        }
    }

    public void OverwriteCSV()
    {
        TextWriter tw = new StreamWriter(filename, false);
        tw.WriteLine("Times Chosen Character 1, Times Chosen Character 2, Times Chosen Map 1, Times Chosen Map 2, Total Time Played, Total Damage Taken, Total Damage Dealt, Total Enemies Killed, Melee Enemies Killed, Ranged Enemies Killed, Total EXP Earned");
        tw.WriteLine(timesChosenChar1.ToString() + "," + timesChosenChar2.ToString() + "," + timesChosenMap1.ToString() + "," + timesChosenMap2.ToString() + "," + totaltimePlayed.ToString() + "," + totalDMGtaken.ToString() + "," + totalDMGgiven.ToString() + "," + totalEnemiesKilled.ToString() + "," + meleeEnemiesKilled.ToString() + "," + rangedEnemiesKilled.ToString() + "," + totalEXPEarned.ToString());
        tw.Close();
    }

}
