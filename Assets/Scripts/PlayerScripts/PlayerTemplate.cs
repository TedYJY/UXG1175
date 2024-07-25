using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Written by Ryan Jacob
[CreateAssetMenu(fileName = "New Character", menuName = "CharacterTemplate")]
public class PlayerTemplate : ScriptableObject //inherit scriptable object
{
    public int characterID;
    public string characterName;
    public int movementSpeed;
    public int health;
    public int maxHealth;
    public int startingLevel;
    public int maxLevel;
    public int startingExp;
    public int currentExp;
    public int expToLevelUp;
    public int expLevel;
    public float expMultiplier;
    public Sprite sprite;
    public int spriteID;
}