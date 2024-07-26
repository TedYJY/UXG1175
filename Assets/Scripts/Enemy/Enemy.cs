using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

//Written by: Tedmund Yap
public class Enemy : MonoBehaviour
{
    private GameObject player; //For accessing scripts, and referencing
    
    public GameObject levelManager;
    LevelManager theLevelManager;

    public float moveSpeed; //Speed of enemy
    public int atkDamage; //Damage of enemy attack
    public float health; //Health of enemy
    public float atkRange; //Range of enemy attack
    public string atkClass; //Class of enemy

    [SerializeField]
    private GameObject enemyProjectile;



    [Header("Searching Variables")]
    [SerializeField]
    private float searchRadius = 5f; //Search radius of the enemy, if player enters this radius, the enemy starts hunting

    public bool foundPlayer; //Whether enemy has found player, to stop roaming and start hunting

    [Header("Roaming Variables")]
    [SerializeField]
    private float roamTimer = 4; //Timer before deciding to roam to new location

    [SerializeField]
    private float roamRadius = 1.5f; //Maximum range of how far enemy will roam

    private Vector2 randomRoam; //Location of where to roam
    private bool changeRoam; //Allows changing of randomRoam

    private bool movementAllowed; //If enemy is able to move

    private float attackCooldownTimer;
    private float attackCooldownMelee;
    private float attackCooldownRanged;

    [SerializeField]
    private Image healthBar; //For health bar UI

    void Start()
    {
        StartMovement(); //Starts roaming or moving to player
        player = GameObject.FindWithTag("Player"); //Moves to player
        attackCooldownMelee = 1.0f;
        attackCooldownRanged = 2.0f;
        attackCooldownTimer = attackCooldownMelee;

        this.GetComponent<CircleCollider2D>().radius = atkRange;
    }

    void Update()
    {
        if (foundPlayer == false) //If player has not been found by search radius
        {
            Roam(); //Start roaming
            Search(); //Start searching for player
        }

        else if (foundPlayer == true) //If player has been found
        {
            Hunt(); //Starts moving towards player
        }

        //For testing enemy HP/clearing enemies
        if (Input.GetKeyDown(KeyCode.P))
        {
            TakeDamage(1);
        }

    }

    void Hunt()
    {
        if (movementAllowed  == true) //Checks if enemy is able to move
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime); //Starts moving towards player
        }
    }

    void Roam()
    {
        if (changeRoam == true) //If change of direction/location is needed
        {
            //Creates random values +- from current position, and assigns to randomRoam (Like roaming sheep but better! Probably :D )
            float randomX = transform.position.x + Random.Range(roamRadius, -roamRadius);
            float randomY = transform.position.y + Random.Range(roamRadius, -roamRadius);
            randomRoam = new Vector2(randomX, randomY);

            //To prevent constant changing of randomRoam
            changeRoam = false;
            roamTimer = 4;
            //Debug.Log("Finding new direction, moving to " +  randomRoam);
        }

        if (changeRoam == false) //If change of direction/location is not needed
        {
            roamTimer -= Time.deltaTime; //Changes every 4 seconds
            if (roamTimer <= 0) //If timer hits 0, change direction/location
            {
                changeRoam = true;
            }
        }

        if (movementAllowed == true) //If able to move
        {
            transform.position = Vector2.MoveTowards(transform.position, randomRoam, moveSpeed * Time.deltaTime); //Move to randomRoam
        }

    }

    void Search()
    {
        if (Vector2.Distance(transform.position, player.transform.position) <= searchRadius) //Checks if player is in search radius
        {
            foundPlayer = true;
        }
    }

    void StartMovement() //Allows movement
    {
        movementAllowed = true;
        changeRoam = true; //Allows new random roam to be created
    }

    void StopMovement() //Stops movement. HALT!
    {
        movementAllowed = false;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            StopMovement();
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            StartMovement();
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            attackCooldownTimer += Time.fixedDeltaTime;

            if (Vector2.Distance(transform.position, player.transform.position) <= searchRadius / 1.2f)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.transform.position, -1 * moveSpeed * Time.deltaTime);
            }

            if (atkClass == "Melee" && attackCooldownTimer >= attackCooldownMelee)
            {
                AttackMelee();
                attackCooldownTimer = 0;
            }

            if (atkClass == "Ranged" && attackCooldownTimer >= attackCooldownRanged)
            {
                AttackRanged();
                attackCooldownTimer = 0;
            }
        }

        
    }

    void AttackMelee()
    {
        player.GetComponent<thePlayer>().TakeDamage(atkDamage);
    }

    void AttackRanged()
    {
        //Shoot ranged projectiles. Pewpew.
        GameObject temp = Instantiate(enemyProjectile, transform.position, transform.rotation);
        EnemyProjectile projectile = temp.GetComponent<EnemyProjectile>();
        projectile.projectileDamage = atkDamage;
        projectile.destination = player.transform.position;
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("enemy took damage" + damage);
        health -= damage;

        //Updates UI for health bar
        healthBar.fillAmount = health / 10f;

        if (health <= 0)
        {
            //In case health overshots to negative values and the UI decides to go insane. Totally did not cause headaches at 5am.
            health = 0;

            //Adds exp to player
            player.GetComponent<thePlayer>().AddToXP(1);

            //Destroys enemy (Or sends to object pool) (To drown.)
            Destroy(this.gameObject);

        }

    }

}
