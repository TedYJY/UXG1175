using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Variables")]
    [SerializeField]
    private EnemySO enSO;
    [SerializeField]
    private GameObject enemyTemplate;
    private GameObject spawnedEnemy;

    public void SpawnEnemy(string enemyName, Vector2 spawnLoc)
    {
        //Change the SO to the given one
        enSO = Resources.Load<EnemySO>(enemyName);

        //Spawn Enemy
        spawnedEnemy = Instantiate(enemyTemplate, spawnLoc, Quaternion.identity);

        //Set Values
        Enemy enemyScript = spawnedEnemy.GetComponent<Enemy>();
        enemyScript.moveSpeed = enSO.enemyMoveSpeed;
        enemyScript.atkDamage = enSO.enemyDMG;
        enemyScript.health = enSO.enemyHP;
        enemyScript.atkRange = enSO.enemyRange;
        enemyScript.atkClass = enSO.enemyClass;
        enemyScript.foundPlayer = true;

        //Set Sprite
        spawnedEnemy.GetComponent<SpriteRenderer>().sprite = enSO.enemySprite;
    }
}
