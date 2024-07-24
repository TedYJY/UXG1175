using UnityEngine;
using UnityEditor;
using System.IO;
using Unity.VisualScripting;

public class CSVtoSO : MonoBehaviour
{
    private static string weaponCSVPath = "/CSVs/WeaponCSVs.csv"; // tell editor where file is
    private static string potionsCSVPath = "/CSVs/ItemCSV.csv";
    private static string spritesPath = "/Sprites/tilemap_packed.png";
    private static string difficultyPath = "/CSVs/DifficultyLevelCSVs.csv";
    private static string characterPath = "/CSVs/PlayerTemplate.csv";


    [MenuItem("Utilities/Generate Weapon")] // creates utility bar top of unity window weapon creator
   public static void GenerateWeapon()

    {

        string[] allLines = File.ReadAllLines(Application.dataPath + weaponCSVPath); // telling program where the file is on the PC

        if (allLines == null)
        {
            Debug.LogError($"CSV file not found at path: {allLines}"); //if csv is not found
            return;
        }

        for (int i = 1; i < allLines.Length; i++) // to read lines from csv
        {
            Debug.Log(allLines[i]);
            string line = allLines[i];
            string[] splitData = line.Split(','); // splits lines of csv by delimiter

            Weapons weapon = ScriptableObject.CreateInstance<Weapons>(); //create new instance of object

            // grabbingthe attributes

            weapon.weaponID = int.Parse(splitData[0]);
            weapon.itemName = splitData[1];
            weapon.type = splitData[2];
            weapon.speed = int.Parse(splitData[3]);
            weapon.damageOutput = int.Parse(splitData[4]);
            weapon.coolDown = int.Parse(splitData[5]);
            weapon.spriteID = int.Parse(splitData[6]);
            weapon.sprites = getSprite(int.Parse(splitData[6]));
           
    

            AssetDatabase.CreateAsset(weapon, $"Assets/SO/Weapons/{weapon.itemName}.asset"); // telling program where to save
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    [MenuItem("Utilities/Generate Potion")] // creates utility bar top of unity window weapon creator
    public static void GeneratePotions()
    {
       
        //Generating Potions
        string[] allLines = File.ReadAllLines(Application.dataPath + potionsCSVPath); // telling program where the file is on the PC

        if (allLines == null)
        {
            Debug.LogError($"CSV file not found at path: {allLines}");
            return;
        }

        for (int i = 1; i < allLines.Length; i++) // to read lines from csv
        {
            Debug.Log(allLines[i]);
            string line = allLines[i];
            string[] splitData = line.Split(','); // splits lines of csv by delimiter

            Potions thePotions = ScriptableObject.CreateInstance<Potions>(); //create new instance of object

            // grabbingthe attributes
            thePotions.itemID = int.Parse(splitData[0]);
            thePotions.itemName = splitData[1];
            thePotions.type = splitData[2];
            thePotions.value = int.Parse(splitData[3]);
            thePotions.duration = int.Parse(splitData[4]);
            //thePotions.sprite = int.Parse(splitData[5]);

            AssetDatabase.CreateAsset(thePotions, $"Assets/SO/Potions/{thePotions.itemName}.asset"); // telling program where to save
        }

        AssetDatabase.SaveAssets();
        
    }

    public static Sprite getSprite(int spriteID) // to pull the sprites from sheet
    {
       
        Sprite[] sprites = Resources.LoadAll<Sprite>(spritesPath); // tell program to load all sprites into array

        string spriteToAssign = $"tilemap_packed_{spriteID}"; // tell program to identify using ID

        Debug.Log("Here");
        
        foreach (Sprite sprite in sprites) 
        {
            if (spriteToAssign == sprite.name)
            {
                Debug.Log("found");
                return sprite;
            }
        }

        Debug.Log("not found");

        return null ; // if not found return nothing
    }

    [MenuItem("Utilities/Generate difficulty")] // creates utility bar top of unity window weapon creator
    public static void GenerateDifficulty()
    {
        
        //Generating difficulty
        string[] allLines = File.ReadAllLines(Application.dataPath + difficultyPath); // telling program where the file is on the PC

        if (allLines == null)
        {
            Debug.LogError($"CSV file not found at path: {allLines}");
            return;
        }

        for (int i = 1; i < allLines.Length; i++) // to read lines from csv
        {
            Debug.Log(allLines[i]);
            string line = allLines[i];
            string[] splitData = line.Split(','); // splits lines of csv by delimiter

            Difficulty theDifficulty = ScriptableObject.CreateInstance<Difficulty>(); //create new instance of object

            // grabbingthe attributes
            theDifficulty.difficultyID = int.Parse(splitData[0]);
            theDifficulty.numberOfSpawners = int.Parse(splitData[1]);
            theDifficulty.maxActiveSpawners = int.Parse(splitData[2]);
            theDifficulty.minActiveSpawners = int.Parse(splitData[3]);
            theDifficulty.numberOfWaves = int.Parse(splitData[4]);
            theDifficulty.waveTimer = int.Parse(splitData[5]);

            AssetDatabase.CreateAsset(theDifficulty, $"Assets/SO/Difficulty/{theDifficulty.difficultyID}.asset"); // telling program where to save
        }

        AssetDatabase.SaveAssets();

    }

    [MenuItem("Utilities/Generate character")] // creates utility bar top of unity window weapon creator
    public static void GenerateCharacter()
    {

        //Generating difficulty
        string[] allLines = File.ReadAllLines(Application.dataPath + characterPath); // telling program where the file is on the PC

        if (allLines == null)
        {
            Debug.LogError($"CSV file not found at path: {allLines}");
            return;
        }

        for (int i = 1; i < allLines.Length; i++) // to read lines from csv
        {
            Debug.Log(allLines[i]);
            string line = allLines[i];
            string[] splitData = line.Split(','); // splits lines of csv by delimiter

            PlayerTemplate thePlayer = ScriptableObject.CreateInstance<PlayerTemplate>(); //create new instance of object

            // grabbingthe attributes
            thePlayer.characterID = int.Parse(splitData[0]);
            thePlayer.characterName = splitData[1];
            thePlayer.movementSpeed = int.Parse(splitData[2]);
            thePlayer.health = int.Parse(splitData[3]);
            thePlayer.maxHealth = int.Parse(splitData[4]);
            thePlayer.startingLevel = int.Parse(splitData[5]);
            thePlayer.maxLevel = int.Parse(splitData[6]);
            thePlayer.startingExp = int.Parse(splitData[7]);
            thePlayer.currentExp = int.Parse(splitData[8]);
            thePlayer.expToLevelUp = int.Parse(splitData[9]);
            thePlayer.expMultiplier = float.Parse(splitData[10]);
            thePlayer.spriteID = int.Parse(splitData[11]);
            thePlayer.sprite = getSprite(int.Parse(splitData[11]));

            AssetDatabase.CreateAsset(thePlayer, $"Assets/SO/Characters/{thePlayer.characterName}.asset"); // telling program where to save
        }

        AssetDatabase.SaveAssets();
    }

}


