using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class thePlayer : MonoBehaviour
{
    // Start is called before the first frame update

    Rigidbody2D rb;
    weaponPickUp theWeapon;

    public Vector2 moveDir;
    public int moveSpeed;

    public float xAxis;
    public float yAxis;

    public int HP;

    public GameObject[] meleeWeapons;
    public GameObject[] rangedWeapons;

    private bool iFrameStatus;
    private float iFrameDuration;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
       
        

        meleeWeapons[0].SetActive(true);
        
    }

    // Update is called once per frame
    void Update()
    {
        InputManagement();
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.tag == "MeleeWeapons2")
        {
            theWeapon = collision.GetComponent<weaponPickUp>();

            meleeWeapons[0].SetActive(false);
            Debug.Log("deactivated");
            meleeWeapons[1].SetActive(true);
            theWeapon.RemoveItem();
        }
        else if (collision.tag == "healthPotion")
        {
            theWeapon = collision.GetComponent<weaponPickUp>();
            HP += theWeapon.HealingPotion();
        }
       
    }


    void FixedUpdate()
    {
        Move();
    }

    //Input Manager
    void InputManagement()
    {
        
        xAxis = Input.GetAxisRaw("Horizontal");
        yAxis = Input.GetAxisRaw("Vertical");

        moveDir = new Vector2(xAxis, yAxis).normalized;
    }

    //Movement
    void Move()
    {
       
        rb.velocity = new Vector2 (moveDir.x * moveSpeed, moveDir.y*moveSpeed);
            
    }



    //Damage taken by Player
    public void TakeDamage(int dmgAmt)
    {
        if (iFrameStatus == false)
        {
            //Reduce HP by dmgAmt
            HP -= dmgAmt;

            //Checks if HP has hit 0, sets to 0 to prevent negative values from showing and does Game Over function
            if (HP <= 0)
            {
                HP = 0;
                //GameOver();
            }

            //Updates UI of HP
            //UIUpdate();

            //Activates iFrames to prevent multiple hits
            //Debug.Log("iFrames activated!");
            iFrameStatus = true;

            //Deactivates iFrame after certain amount of time
            Invoke("iFrameCountdown", iFrameDuration);

        }
    }

    void iFrameCountdown()
    {
        //Debug.Log("iFrames deactivated!");
        iFrameStatus = false;
    }

   
   
}
