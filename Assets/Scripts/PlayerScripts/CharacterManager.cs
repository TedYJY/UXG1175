using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterManager : MonoBehaviour
{
    public CharacterDataBase characterData; // accessing the chracter database for amount of characters
    //thePlayer playerscript;

    public Text characterText; //displays character data and name
    public SpriteRenderer theSprite;

    public int selectedOption = 0; //set to default

    void Start()
    {
        UpdateCharacter(selectedOption);
    }


    public void NextChar()
    {
        selectedOption++;

        if (selectedOption >= characterData.CharacterCount) 
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
            selectedOption = characterData .CharacterCount - 1;
        }

        UpdateCharacter(selectedOption);
    }

    public void Play()
    {
        SceneManager.LoadScene("GameScene");
    }

    private void UpdateCharacter(int selectedOption)
    {
        PlayerTemplate character =  characterData.GetCharacterData(selectedOption); // calling character template to assign the SO to
        theSprite.sprite = character.sprite;
        characterText.text = character.characterName;

        CharacterSelectionManager.SelectedCharacterIndex = selectedOption; // saving the option chosen
    }
}
