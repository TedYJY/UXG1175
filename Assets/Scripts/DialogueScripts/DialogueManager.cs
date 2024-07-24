using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity;
using UnityEditor;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

        currentDialogue = inputSO;

        if (inputSO != null)
        {
            SpeakerImage.GetComponent<Image>().sprite = inputSO.speakerSprite;
            SpeakerText.GetComponent<TextMeshProUGUI>().text = inputSO.speakerName;
            dialogueText.GetComponent<TextMeshProUGUI>().text = inputSO.speakerDialogue;

            if (inputSO.hasChoice == true)
            {
                choiceButton1.SetActive(true);
                choiceButton2.SetActive(true);
                nextButton.SetActive(false);

                foreach (string i in inputSO.dialogueChoiceIDs)
                {
                    Debug.Log(i);
                }

                choice1SO = Resources.Load<DialogueSO>(inputSO.dialogueChoiceIDs[0]);
                choice2SO = Resources.Load<DialogueSO>(inputSO.dialogueChoiceIDs[1]);
                cb1Text.GetComponent<TextMeshProUGUI>().text = choice1SO.speakerDialogue;
                cb2Text.GetComponent<TextMeshProUGUI>().text = choice2SO.speakerDialogue;

            }

            else
            {

                choiceButton1.SetActive(false);
                choiceButton2.SetActive(false);
                nextButton.SetActive(true);

                
                nextSO = Resources.Load<DialogueSO>(inputSO.dialogueChoiceIDs[0]);
            }



        }

    }

    public void ChooseChoice1()
    {
        if (choice1SO != null)
        {
            ChangeDialogue(choice1SO);
        }
    }

    public void ChooseChoice2()
    {
        if (choice2SO != null)
        {
            ChangeDialogue(choice2SO);
        }
    }

    public void NextDialogue()
    {
        if (nextSO != null)
        {
            ChangeDialogue(nextSO);
        }

        else
        {
            SceneManager.LoadScene(1);
        }

    }
}
