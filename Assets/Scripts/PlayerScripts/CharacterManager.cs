using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//Written by Ryan Jacob
public class CharacterManager : MonoBehaviour
{
    public CharacterDataBase characterData; // accessing the chracter database for amount of characters
    //thePlayer playerscript;

    public Text characterText; //displays character data and name
    public SpriteRenderer theSprite;

    public int selectedOption = 0; //set to default

    void Start()
    {
        UpdateCharacter(selectedOption); //set default character selected to 0
    }


    public void NextChar()
    {
        selectedOption++;

        if (selectedOption >= characterData.CharacterCount) // if selected option is more than character count loop back to first char
        {
         selectedOption = 0;
        }

        UpdateCharacter(selectedOption);
    }


    public void prevChar()
    {
        selectedOption--;

        if (selectedOption < 0)
        {
            selectedOption = characterData .CharacterCount - 1; // if selected option is more than character count loop back to last
        }

        UpdateCharacter(selectedOption);
    }


    private void UpdateCharacter(int selectedOption)
    {
        PlayerTemplate character =  characterData.GetCharacterData(selectedOption); // calling character template to assign the SO to
        theSprite.sprite = character.sprite; //update the sprite and text to display SO sprite and name
        characterText.text = character.characterName;

        CharacterSelectionManager.SelectedCharacterIndex = selectedOption; // saving the option chosen
    }
}
