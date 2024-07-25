using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Written by Ryan Jacob
[CreateAssetMenu(fileName = "NewCharacterDataBase", menuName = "CharacterDataBase")]
public class CharacterDataBase : ScriptableObject
{
    public PlayerTemplate[] characters;// list of avialable characters in the game

    public int CharacterCount
    {
        get
        {
            return characters.Length; // getting total amount of characters
        }
    }

    public PlayerTemplate GetCharacterData(int characterID)
    {
        return characters[characterID]; // getting specific character
    }

}
