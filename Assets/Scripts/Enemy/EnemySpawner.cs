using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Variables")]
    [SerializeField]
    private EnemySO enemySO;
    [SerializeField]
    private GameObject enemyTemplate;

    void SpawnEnemy(EnemySO inputSO, Vector2 spawnLoc)
    {
        //Change the SO to the given one
        enemySO = inputSO;

        //Set Values
        Enemy enemyScript = enemyTemplate.GetComponent<Enemy>();
        enemyScript.moveSpeed = enemySO.enemyMoveSpeed;
        enemyScript.atkDamage = enemySO.enemyDMG;
        enemyScript.health = enemySO.enemyHP;
        enemyScript.atkRange = enemySO.enemyRange;
        enemyScript.atkClass = enemySO.enemyClass;

        //Set Sprite
        enemyTemplate.GetComponent<SpriteRenderer>().sprite = enemySO.enemySprite;

        //Spawn Enemy
        Instantiate(enemyTemplate, spawnLoc, Quaternion.identity);
    }
}
