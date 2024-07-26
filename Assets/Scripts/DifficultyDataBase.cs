using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDifficultyDataBase", menuName = "DifficultyDataBase")]
public class DifficultyDataBase : ScriptableObject
{
    public Difficulty[] difficulty;// list of avialable difficulty in the game

    public int DifficultyCount
    {
        get
        {
            return difficulty.Length; // getting total amount of characters
        }
    }

    public Difficulty GetDiffulcutyData(int difficultyID)
    {
        return difficulty[difficultyID]; // getting specific difficulty
    }
}
