using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "CharacterTemplate")]
public class PlayerTemplate : ScriptableObject //inherit scriptable object
{
    public int characterID;
    public string characterName;
    public int movementSpeed;
    public int health;
    public int startingLevel;
    public int maxLevel;
    public int startingExp;
    public int expLevelUp;
    public int expLevel;
    public float expMultiplier;
    public Sprite sprite;
}