using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Written by Ryan Jacob
public class UIManager : MonoBehaviour
{
    
    thePlayer characterInfo;
    LevelManager gameManager;

    public GameObject thePlayer; //reference to gameObjects in scene
    public GameObject WaveSystem;

    public Text waveText; // reference to UI game Objects
    public Text healthText;
    public Text expText;
    public Text timeText;
    public Text playerLvl;
    public Text enemiesRemaining;


    // Start is called before the first frame update
    void Start()
    {
        characterInfo = thePlayer.GetComponent<thePlayer>();
        gameManager = WaveSystem.GetComponent<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        waveText.text= "Wave No: " + gameManager.currentWave.ToString();                                     //update UI components
        healthText.text = string.Format("Health = {0}/{1}", characterInfo.hp,characterInfo.maxHP);
        expText.text = string.Format("Exp: {0}/{1}", characterInfo.currentExp, characterInfo.expToLevelUp);
        timeText.text = "Time Elapsed: " + gameManager.totalElapsedTime.ToString();
        playerLvl.text = "Player Level: " + characterInfo.startingLevel.ToString();
        enemiesRemaining.text = "Enemies remaining: " + gameManager.enemiesRemaining.ToString();

        int minutes = gameManager.totalElapsedTime / 60; //calculating playtime
        int seconds = gameManager.totalElapsedTime % 60;

        if (minutes > 0) //convert to minutes and seconds
        {
            timeText.text = string.Format("Time Elapsed: {0}:{1}}", minutes, seconds);
        }
        else // keep seconds if play time is below a minute
        {
            timeText.text = string.Format("Time Elapsed: {0} sec", seconds);
        }
    }
}