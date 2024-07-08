using System.IO;
using UnityEngine;
using UnityEditor;

public class EnemyCSVtoSO
{
    //String path for the CSV for enemies
    //Follow naming conventions, else errors show up
    private static string CSVpath = "/Editor/CSV/enemyData.csv";

    //Adds Menu Item to the Inspector Window to allow generation of new enemies
    [MenuItem("Utilities/Generate Enemies")]

    public static void GenerateEnemies()
    {
        //Uses ReadAllLines function to seperate CSV based on rows
        //Uses Application.dataPath to ensure that the CSV can be found regardless on project folder location
        string[] allLines = File.ReadAllLines(Application.dataPath + CSVpath);

        //Loops through the String of allLines, skipping past first and last line (First line is the headers, bottom line CSV leaves a blank line by default)
        for (int i = 1; i < allLines.Length; i++)
        {

            //Seperates into new Strings using the comma (',') delimiter
            string[] splitLines = allLines[i].Split(new char[] { ','});


            //Checks if all data entries are filled
            if(splitLines.Length != 7)
            {
                Debug.Log(allLines[i] + " does not have all 6 values!");

                //Returns back if all data entries are not filled for "Mental Stability" purposes
                return;
            }

            //Creates new Scriptable Object by referencing to EnemySO script
            EnemySO enemy = ScriptableObject.CreateInstance<EnemySO>();

            //Passes data from CSV into the SO
            enemy.enemyName = splitLines[0];
            enemy.enemyHP = int.Parse(splitLines[1]);
            enemy.enemyDMG = int.Parse(splitLines[2]);
            enemy.enemyMoveSpeed = float.Parse(splitLines[3]);
            enemy.enemyRange = float.Parse(splitLines[4]);
            enemy.enemyClass = splitLines[5];

            //Uses resources.load to get the entire spritesheet
            Sprite[] sprites = Resources.LoadAll<Sprite>("tilemap_packed");

            //Uses number in CSV to get specific sprite
            enemy.enemySprite = sprites[int.Parse(splitLines[6])];

            //Creates a new SO using AssetDatabase CreateAsset function
            //Ensure that there is a folder under Assets labelled "Enemies", else an error will show up
            AssetDatabase.CreateAsset(enemy, $"Assets/ScriptableObjects/Enemies/Resources/{enemy.enemyName}.asset");
        }

        //Saves the created assets
        AssetDatabase.SaveAssets();

    }
}
