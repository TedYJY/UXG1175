using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Written by: Tedmund Yap
public class DialogueSO : ScriptableObject
{
    public string dialogueID;
    public string dialogueGroupID;
    public int speakerID;
    public string speakerName;
    public string speakerDialogue;
    public Sprite speakerSprite;
    public string[] dialogueChoiceIDs;
    public bool hasChoice;
}
