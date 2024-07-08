using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private GameObject player; //For accessing scripts, and referencing
    public float moveSpeed; //Speed of enemy
    public int atkDamage; //Damage of enemy attack
    public float health; //Health of enemy
    public float atkRange; //Range of enemy attack
    public string atkClass; //Class of enemy

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

    private bool attackAble; //If enemy is able to attack

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
            //Creates random values +- from current position, and assigns to randomRoam
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
            roamTimer -= Time.deltaTime; //Changes every 4 secodns
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

    void StopMovement() //Stops movement
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
            if (attackAble == true)
            {
                AttemptAttack();
            }
        }
    }

    void AttemptAttack()
    {
        if (atkClass == "Melee")
        {
            AttackMelee();
        }

        else if (atkClass == "Ranged")
        {
            AttackRanged();
        }
    }

    void AttackMelee()
    {
        player.GetComponent<thePlayer>().TakeDamage(atkDamage);
    }

    void AttackRanged()
    {
        //Shoot ranged projectiles
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            health = 0;
            Destroy(this.gameObject);
        }

        //Update UI
    }

}
