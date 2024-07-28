using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

//Written by: Tedmund Yap
public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Variables")]
    [SerializeField]
    private EnemySO enSO;
    [SerializeField]
    private GameObject enemyTemplate;
    private GameObject spawnedEnemy;

    public void SpawnEnemy(string enemyID, Vector2 spawnLoc)
    {
        //Change the SO to the given one
        enSO = Resources.Load<EnemySO>(enemyID);

        //Spawn Enemy
        spawnedEnemy = Instantiate(enemyTemplate, spawnLoc, Quaternion.identity);

        //Set Values
        Enemy enemyScript = spawnedEnemy.GetComponent<Enemy>();
        enemyScript.moveSpeed = enSO.enemyMoveSpeed;
        enemyScript.atkDamage = enSO.enemyDMG;
        enemyScript.health = enSO.enemyHP;
        enemyScript.atkRange = enSO.enemyRange;
        enemyScript.atkClass = enSO.enemyClass;
        enemyScript.dropChance = enSO.enemyDropChance;
        enemyScript.expAmt = enSO.enemyExpAmt;

        //Set spawned enemy to instantly go after player
        enemyScript.foundPlayer = true;

        //Set Sprite
        spawnedEnemy.GetComponent<SpriteRenderer>().sprite = enSO.enemySprite;

        //Set Name
        spawnedEnemy.name = enSO.enemyName;
    }
}
