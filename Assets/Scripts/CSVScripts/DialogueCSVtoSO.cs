using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;

//Written by: Tedmund Yap
public class DialogueCSVtoSO : MonoBehaviour
{
    private static string CSVpath = "/CSVs/Dialogue.csv";

    //Adds Menu Item to the Inspector Window to allow generation of new enemies
    [MenuItem("Utilities/Generate Dialogue")]

    public static void GenerateDialogue()
    {
        //Uses ReadAllLines function to seperate CSV based on rows
        //Uses Application.dataPath to ensure that the CSV can be found regardless on project folder location
        string[] allLines = File.ReadAllLines(Application.dataPath + CSVpath);

        //Uses resources.load to get the entire spritesheet
        Sprite[] sprites = Resources.LoadAll<Sprite>("tilemap_packed");

        //Loops through the String of allLines, skipping past first and last line (First line is the headers, bottom line CSV leaves a blank line by default)
        for (int i = 1; i < allLines.Length; i++)
        {

            //Seperates into new Strings using the comma (',') delimiter
            string[] splitLines = allLines[i].Split(new char[] { ',' });


            //Checks if all data entries are filled
            if (splitLines.Length != 9)
            {
                Debug.Log(allLines[i] + " does not have all 9 values!");

                //Returns back if all data entries are not filled for "Mental Stability" purposes
                return;
            }

            //Creates new Scriptable Object by referencing to DialogueSO script
            DialogueSO dialogueSO = ScriptableObject.CreateInstance<DialogueSO>();

            //Passes data from CSV into the SO
            dialogueSO.dialogueID = splitLines[0];
            dialogueSO.dialogueGroupID = splitLines[1];
            dialogueSO.speakerID = int.Parse(splitLines[2]);
            dialogueSO.speakerName = splitLines[3];
            dialogueSO.listenerName = splitLines[4];
            dialogueSO.speakerDialogue = splitLines[5];

            string[] dialogueChoicesSplit = splitLines[8].Split(new char[] { '#' });

            //Checks if dialogue has choices
            dialogueSO.dialogueChoiceIDs = dialogueChoicesSplit;

            dialogueSO.hasChoice = dialogueChoicesSplit.Length > 1 ? true : false;



            //Uses number in CSV to get specific sprite
            dialogueSO.speakerSprite = sprites[int.Parse(splitLines[6])];
            dialogueSO.listenerSprite = sprites[int.Parse(splitLines[7])];

            //Creates a new SO using AssetDatabase CreateAsset function
            //Ensure that there is a folder under Assets labelled "Dialogue", else an error will show up
            AssetDatabase.CreateAsset(dialogueSO, $"Assets/SO/Dialogue/Resources/{splitLines[1]}.asset");
        }

        //Saves the created assets
        AssetDatabase.SaveAssets();

    }
}
