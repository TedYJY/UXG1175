using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using UnityEngine.SceneManagement;

public class Statistics : MonoBehaviour
{
    [SerializeField]
    private GameObject chosenChar1;
    [SerializeField]
    private GameObject chosenChar2;
    [SerializeField]
    private GameObject chosenMap1;
    [SerializeField]
    private GameObject chosenMap2;
    [SerializeField]
    private GameObject timePlayed;
    [SerializeField]
    private GameObject totalDamageTaken;
    [SerializeField]
    private GameObject totalDamageGiven;
    [SerializeField]
    private GameObject totalEnemies;
    [SerializeField]
    private GameObject melee;
    [SerializeField]
    private GameObject ranged;
    [SerializeField]
    private GameObject exp;

    private void Awake()
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
                chosenChar1.GetComponent<TextMeshProUGUI>().text = "Chose Bruiser " + (splitData[0]) + " times.";
                chosenChar2.GetComponent<TextMeshProUGUI>().text = "Chose Wreched " + (splitData[1]) + " times.";
                chosenMap1.GetComponent<TextMeshProUGUI>().text = "Played on Crazed Castle " + (splitData[2]) + " times.";
                chosenMap2.GetComponent<TextMeshProUGUI>().text = "Played on Grave Danger " + (splitData[3]) + " times.";
                timePlayed.GetComponent<TextMeshProUGUI>().text = "Lost " + (splitData[4]) + " seconds of your life playing this.";
                totalDamageTaken.GetComponent<TextMeshProUGUI>().text = "Received (involuntarily) " + (splitData[5]) + " points of damage.";
                totalDamageGiven.GetComponent<TextMeshProUGUI>().text = "Dealt (voluntarily) " + (splitData[6]) + " points of damage.";
                totalEnemies.GetComponent<TextMeshProUGUI>().text = "Slain " + (splitData[7]) + " number of enemies in total.";
                melee.GetComponent<TextMeshProUGUI>().text = "Slain " + (splitData[8]) + " number of Melee enemies.";
                ranged.GetComponent<TextMeshProUGUI>().text = "Slain " + (splitData[9]) + " number of Ranged enemies.";
                exp.GetComponent<TextMeshProUGUI>().text = "Received " + (splitData[10]) + " amount of EXP.";

        }

    }

    public void Continue()
    {
        SceneManager.LoadScene("CharacterSelect");
    }


}


