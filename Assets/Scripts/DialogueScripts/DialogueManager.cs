using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity;
using UnityEditor;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//Written by: Tedmund Yap
public class DialogueManager : MonoBehaviour
{

    [SerializeField]
    private GameObject SpeakerImage;
    [SerializeField]
    private GameObject SpeakerText;
    [SerializeField]
    private GameObject dialogueText;
    [SerializeField]
    private GameObject choiceButton1;
    [SerializeField]
    private GameObject choiceButton2;
    [SerializeField]
    private GameObject cb1Text;
    [SerializeField]
    private GameObject cb2Text;
    [SerializeField]
    private GameObject nextButton;

    [SerializeField]
    private DialogueSO currentDialogue;

    private DialogueSO choice1SO;
    private DialogueSO choice2SO;
    private DialogueSO nextSO;

    // Start is called before the first frame update
    void Awake()
    {
        ChangeDialogue(currentDialogue);
    }

    public void ChangeDialogue(DialogueSO inputSO)
    {
        //Changes dialogue to scriptable object that was given to the function
        currentDialogue = inputSO;

        //In case the SO is empty. Pls dont be empty.
        if (inputSO != null)
        {
            //Replaces image, speaker name and the dialogue
            SpeakerImage.GetComponent<Image>().sprite = inputSO.speakerSprite;
            SpeakerText.GetComponent<TextMeshProUGUI>().text = inputSO.speakerName;
            dialogueText.GetComponent<TextMeshProUGUI>().text = inputSO.speakerDialogue;

            //Checks if you have a say in whatever the game wants to yap about
            if (inputSO.hasChoice == true)
            {
                //Gives choice! FREEDOM!
                choiceButton1.SetActive(true);
                choiceButton2.SetActive(true);
                nextButton.SetActive(false);
                choice1SO = Resources.Load<DialogueSO>(inputSO.dialogueChoiceIDs[0]);
                choice2SO = Resources.Load<DialogueSO>(inputSO.dialogueChoiceIDs[1]);
                cb1Text.GetComponent<TextMeshProUGUI>().text = choice1SO.speakerDialogue;
                cb2Text.GetComponent<TextMeshProUGUI>().text = choice2SO.speakerDialogue;

            }

            else
            {
                //No choice. Oppression.
                choiceButton1.SetActive(false);
                choiceButton2.SetActive(false);
                nextButton.SetActive(true);

                //Sets next SO to be loaded by the next button
                nextSO = Resources.Load<DialogueSO>(inputSO.dialogueChoiceIDs[0]);
            }



        }

    }

    public void ChooseChoice1()
    {
        //Checks if choice1SO is not empty. Take the blue pill, the story ends, wake up in bed and believe whatever I want to believe.
        if (choice1SO != null)
        {
            ChangeDialogue(choice1SO);
        }
    }

    public void ChooseChoice2()
    {
        //Checks if choice2SO is not empty. Take the red pill, stay in wonderland, and be shown how deep the rabbithole goes.
        if (choice2SO != null)
        {
            ChangeDialogue(choice2SO);
        }
    }

    public void NextDialogue()
    {
        //Checks if nextSO is null and if it isn't, load the next dialogue
        if (nextSO != null)
        {
            ChangeDialogue(nextSO);
        }

        //Loads the character select scene because I wanna play the game now.
        else
        {
            SceneManager.LoadScene(1);
        }

    }
}
