using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform target;

    public float speed;
    public float health = 100;

    

    //MeleeWeapons M_weapon;

    void Start()
    {
        target.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed*Time.deltaTime);
    }

    public void TakeDamage(int damage)
    {
        //Debug.Log("Input Received");
        health -= damage;

        if (health < 0)
        {
            Destroy(this.gameObject);
        }

    }

}
