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
    public float dropChance; // Item dropChance for enemy
    public int expAmt; //Amount of exp for enemy

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
    private float minXRoam; //Left boundary of map

    [SerializeField]
    private float maxXRoam; //Right boundary of map

    [SerializeField]
    private float minYRoam; //Bottom boundary of map

    [SerializeField]
    private float maxYRoam; //Top boundary of map

    [SerializeField]
    private float roamRadius = 1.5f; //Maximum range of how far enemy will roam

    private Vector2 randomRoam; //Location of where to roam
    private bool changeRoam; //Allows changing of randomRoam

    private bool movementAllowed; //If enemy is able to move

    private float attackCooldownTimer = 3f; //Base cooldown amount for enemies
    private float attackCooldownMelee = 1.0f; //Additional cooldown timer for melee units
    private float attackCooldownRanged = 3f; //Addtional cooldown timer for ranged units

    [SerializeField]
    private Image healthBar; //For health bar UI

    void Start()
    {
        StartMovement(); //Starts roaming or moving to player
        player = GameObject.FindWithTag("Player"); //Moves to player
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
            TakeDamage(20);
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

            if (randomX <= minXRoam || randomX >= maxXRoam || randomY <= minYRoam || randomY >= maxYRoam)
            {
                return;
            }

            randomRoam = new Vector2(randomX, randomY);

            //To prevent constant changing of randomRoam
            changeRoam = false;
            roamTimer = Random.Range(2, 6);
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
            StopMovement(); //Stops movement when the player is in the enemy's attack range
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            StartMovement(); //Starts movement when outside of their attack range
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            attackCooldownTimer += Time.fixedDeltaTime; // Timer for the cooldown of attacks to prevent

            if (atkClass == "Melee" && attackCooldownTimer >= attackCooldownMelee)
            {
                AttackMelee();
                attackCooldownTimer = 0; //Starts cooldown
            }

            if (atkClass == "Ranged" && attackCooldownTimer >= attackCooldownRanged)
            {
                AttackRanged();
                attackCooldownTimer = 0; //Starts cooldown
            }

            if (Vector2.Distance(transform.position, player.transform.position) <= searchRadius / 1.4f)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.transform.position, -1 * moveSpeed * Time.deltaTime); //For ranged units to start running when the player approaches them because... I would also run if someone ran at me with flying knives
            }
        }

        
    }

    void AttackMelee()
    {
        player.GetComponent<thePlayer>().TakeDamage(atkDamage); //Directly attack the player. Bonk.
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
        foundPlayer = true; //For roaming enemies

        health -= damage;

        //Updates UI for health bar
        healthBar.fillAmount = health / 100f; //Assume that base HP of enemies have to be above 10 and below 100 or else the UI will glitch out

        if (health <= 0)
        {
            //In case health overshots to negative values and the UI decides to go insane. Totally did not cause headaches at 5am.
            health = 0;

            //Adds exp to player
            switch (atkClass)
            {
                case "Melee":
                    player.GetComponent<thePlayer>().AddToXP(1);
                    break;

                case "Ranged":
                    player.GetComponent<thePlayer>().AddToXP(2);
                    break;

                default:
                    break;
            }

            DropItem(); //Attempts to drop an item

            //Destroys enemy (Or sends to object pool) (To drown.)
            Destroy(this.gameObject);

        }

    }

    void DropItem()
    {
        float percentageChance = dropChance * 100;
        int random = Random.Range(0, 100);

        if (random <= percentageChance)
        {
            Instantiate(healthBar, transform.position, transform.rotation);
        }

    }

}
