using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveHandler : MonoBehaviour
{
    private GameObject spawnHandler;
    private EnemySpawner spawner;

    // Start is called before the first frame update
    void Start()
    {
        spawnHandler = GameObject.FindWithTag("SpawnHandler");
        spawner = spawnHandler.GetComponent<EnemySpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            spawner.SpawnEnemy("Scout", new Vector2(0, 0));
        }

        else if (Input.GetKeyDown(KeyCode.O))
        {
            spawner.SpawnEnemy("Tank", new Vector2(0, 0));
        }

        else if (Input.GetKeyDown(KeyCode.P))
        {
            spawner.SpawnEnemy("Mage", new Vector2(0, 0));
        }
    }

}
