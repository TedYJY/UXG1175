using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

//Written by Ryan Jacob
public class ProjectileSpawner : MonoBehaviour
{
    public Transform spawnPoint;

    public GameObject projectile;
    public GameObject[] targets;


    public int spawnCoolDown = 2;
    
    public ProjectileTemplate projectileTemplate;

    public bool isCooldown;


    // Start is called before the first frame update
    void Start()
    {
        isCooldown = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isCooldown == false)
        {
            StartCoroutine(StartSpawning(spawnCoolDown));
        }
    }

    void SpawnProjectiles()
    { 
            for (int i = 0; i < targets.Length; i++) // spawn projectiles based on spawn amount
            {
                projectileTemplate = projectile.GetComponent<ProjectileTemplate>(); //referencing the script in the projectile
                Instantiate(projectile, spawnPoint.position,Quaternion.identity); //spawn to player
                projectileTemplate.SetTarget(targets[i].transform.position); // assign target to the new projectile

            }
    }

    IEnumerator StartSpawning(int spawnCooldown) //spawn controller for the projectile
    {
        isCooldown = true; //set the cooldown
        yield return new WaitForSeconds(spawnCooldown); // timer for cooldown
        SpawnProjectiles(); // call spawn projectile
        isCooldown = false; //reset cool down
    }

}
