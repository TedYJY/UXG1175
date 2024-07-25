using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Written by Ryan Jacob
[CreateAssetMenu(fileName = "New Difficulty", menuName = "DifficultyState")]
public class Difficulty : ScriptableObject //inherit scriptable object
{
    public int difficultyID;
    public int numberOfSpawners;
    public int maxActiveSpawners;
    public int minActiveSpawners;
    public int numberOfWaves;
    public float waveTimer;
}
