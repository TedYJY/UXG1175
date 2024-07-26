using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


//Written by Ryan Jacob
public class DifficultyManager : MonoBehaviour
{
    public DifficultyDataBase DifficultyData; // accessing the chracter database for amount of characters

    public Text DifficultyText;
   
    public int selectedOption = 0; //set to default

    void Start()
    {
        UpdateDifficulty(selectedOption);
    }


    public void NextDifficulty()
    {
        selectedOption++;

        if (selectedOption >= DifficultyData.DifficultyCount)
        {
            selectedOption = 0;
        }

        UpdateDifficulty(selectedOption);
    }


    public void PrevDifficulty()
    {
        selectedOption--;

        if (selectedOption < 0)
        {
            selectedOption = DifficultyData.DifficultyCount - 1;
        }

        UpdateDifficulty(selectedOption);
    }

    public void Play()
    {
        SceneManager.LoadScene("GameScene");
    }

    private void UpdateDifficulty(int selectedOption)
    {
        Difficulty theDifficulty = DifficultyData.GetDiffulcutyData(selectedOption); // calling character template to assign the SO to
        
        DifficultyText.text = theDifficulty.difficultyName;

        CharacterSelectionManager.SelectedDifficultyIndex = selectedOption; // saving the option chosen
    }
}
