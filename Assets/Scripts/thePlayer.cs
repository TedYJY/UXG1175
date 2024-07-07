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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        string tag = collision.gameObject.tag;

        if (tag == "Enemy")
        {
            TakeDamage();
        }
        else if (tag == "MeleeWeapons2")
        {
            theWeapon = collision.GetComponent<weaponPickUp>();

            meleeWeapons[0].SetActive(false);
            Debug.Log("deactivated");
            meleeWeapons[1].SetActive(true);
            theWeapon.RemoveItem();
        }
        else if (tag == "healthPotion")
        {
            theWeapon = collision.GetComponent<weaponPickUp>();
            HP += theWeapon.HealingPotion();
        }
       
    }


    private void FixedUpdate()
    {
        Move();
    }

    void InputManagement()
    {
        
        xAxis = Input.GetAxisRaw("Horizontal");
        yAxis = Input.GetAxisRaw("Vertical");

        moveDir = new Vector2(xAxis, yAxis).normalized;
    }

    void Move()
    {
       
        rb.velocity = new Vector2 (moveDir.x * moveSpeed, moveDir.y*moveSpeed);
            
    }

    void TakeDamage()
    {
        if (HP >= 0)
        {
            HP -= 1;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

   
   
}
