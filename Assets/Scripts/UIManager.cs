using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    thePlayer characterInfo;
    EnemySpawnPoints wave;

    public Text waveText;
    public Text healthText;
    public Text expText;
    // Start is called before the first frame update
    void Start()
    {
        wave = GetComponent<EnemySpawnPoints>();
        characterInfo = GetComponent<thePlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        /*waveText.text = "Wave No: " + wave.currentWave.ToString();
        healthText.text = "Health = " + characterInfo.hp.ToString();
        expText.text = string.Format("Exp: {0}/{1}", characterInfo.startingExp, characterInfo.expLevelUp);*/
    }
}