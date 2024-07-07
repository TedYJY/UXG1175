using UnityEngine;
using UnityEditor;
using System.IO;

public class CSVtoSO : MonoBehaviour
{
    private static string weaponCSVPath = "/CSVs/itemCSVs.csv"; // tell editor where file is
    private static string potionsCSVPath = "/CSVs/healthPotionCSV.csv";

    [MenuItem("Utilities/Generate Weapon")] // creates utility bar top of unity window weapon creator
    [MenuItem("Utilities/Generate Potion")] // creates utility bar top of unity window weapon creator
   

    public static void GenerateWeapon()
    {
        string[] allLines = File.ReadAllLines(Application.dataPath + weaponCSVPath); // telling program where the file is on the PC

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

            MeleeWeapons m_Weapon = ScriptableObject.CreateInstance<MeleeWeapons>(); //create new instance of object

            // grabbingthe attributes
            m_Weapon.itemName = splitData[0];
            m_Weapon.rotationSpeed = int.Parse(splitData[1]);
            m_Weapon.damageOutput = int.Parse(splitData[2]);

            AssetDatabase.CreateAsset(m_Weapon, $"Assets/ItemScriptholders/Weapons/{m_Weapon.itemName}.asset"); // telling program where to save
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

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
            string line = allLines[i];
            string[] splitData = line.Split(','); // splits lines of csv by delimiter

            Potions thePotions = ScriptableObject.CreateInstance<Potions>(); //create new instance of object

            // grabbingthe attributes
            thePotions.itemName = splitData[0];
            thePotions.type = splitData[1];
            thePotions.value = int.Parse(splitData[2]);

            AssetDatabase.CreateAsset(thePotions, $"Assets/ItemScriptholders/Potions/{thePotions.itemName}.asset"); // telling program where to save
        }

        AssetDatabase.SaveAssets();
        
    }


}
