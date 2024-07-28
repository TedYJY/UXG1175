using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Written By: Ryan Jacob && Tedmund Yap
public class LevelSelection : MonoBehaviour
{
    //references
    CharacterManager playGame;
    public GameObject playButton;

    public int levelIndex; //set to default
    public int selectedIndex;

    [SerializeField]
    private GameObject statTracker; //For analytics

    // Start is called before the first frame update

    public void Start()
    {
        selectedIndex = 0;
        statTracker = GameObject.FindWithTag("Stats Tracker");
    }

    public void LoadLevel(int levelIndex) // set index under the button UI
    {
       
        selectedIndex = levelIndex;
        //Debug.Log(selectedIndex);

    }

    public void Play() //get index load scene based on index form button
    {
        if (selectedIndex == 0)
        {
            SceneManager.LoadScene("GameScene1");
            statTracker.GetComponent<StatsTracker>().timesChosenMap1 += 1;

        }
        else
        {
            SceneManager.LoadScene("GameScene2");
            statTracker.GetComponent<StatsTracker>().timesChosenMap2 += 1;
        }


    }
}
