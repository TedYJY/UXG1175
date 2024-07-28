using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// By Ryan Jacob
public class LevelSelection : MonoBehaviour
{
    //references
    CharacterManager playGame;
    public GameObject playButton;

    public int levelIndex; //set to default
    public int selectedIndex;

    // Start is called before the first frame update

    public void Start()
    {
        selectedIndex = 0;
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
        }
        else
        {
            SceneManager.LoadScene("GameScene2");
        }


    }
}
