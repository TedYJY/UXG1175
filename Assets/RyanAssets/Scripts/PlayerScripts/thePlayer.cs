using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class thePlayer : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    PlayerTemplate characters;

    public PlayerTemplate[] charList;


    Rigidbody2D rb;
    ItemPickUp theItem;
    MeleeWeaponTest weapon;
    ProjectileSpawner projectile;


    //movement 
    public Vector2 moveDir;

    //variables inherited from SO
    public int characterID;
    public int moveSpeed;
    public int hp;
    public int startingLevel;
    public int maxLevel;
    public int startingExp;
    public int expLevelUp;
    public float expMultiplier;


    public float xAxis;
    public float yAxis;

    

    // for weapons
    public GameObject meleeWeaponHolder;
    public GameObject Projectile; // actual projectile
    public GameObject projectileHolder;
    public GameObject weaponPivot;

    // item variables
    private int itemID;
    private int itemValue;
    private int itemDuration;
    private string type;
    private int spriteID;
    public Sprite sprite;

    // additional variables for weapons
    private int speed;
    private int damageOutput;




    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();

        int selectedCharacterIndex = CharacterSelectionManager.SelectedCharacterIndex; //getting selected chracter index from selection manager

        characters = charList[selectedCharacterIndex]; //getting data from selectced character

        //re assigning values to player script from SO
        characterID = characters.characterID;
        moveSpeed = (int)characters.movementSpeed;
        hp = characters.health;
        startingLevel = characters.startingLevel;
        maxLevel = (int)characters.maxLevel;
        startingExp = characters.startingExp;
        expLevelUp = characters.expLevelUp;
        expMultiplier = characters.expMultiplier;
        sprite = characters.sprite;

        SwapSprite(characterID);
       
    }

    // Update is called once per frame
    void Update()
    {
        InputManagement();
        
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
        if (hp >= 0)
        {
            hp -= 1;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private IEnumerator HandleSpeedPotion(int value, int timer) //speed potion timer, gets value from Item Pick Up script
    {
        moveSpeed += value;
        yield return new WaitForSeconds(timer);
        moveSpeed -= value;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        
        theItem = collision.GetComponent<ItemPickUp>();
        
        if (theItem.tag == "normalItems")
        {
            var ItemData = theItem.GrabItemValues(); // storing the values returned from the grabID method for items;

            itemID = ItemData.itemID; //grabbing the various dats
            itemValue = ItemData.value;
            itemDuration = ItemData.itemTimer;
            type = ItemData.type;


            ItemFunctions(itemID); // send to excute functions
        }

        else if (theItem.tag == "Weapon")
        {
            var weaponData = theItem.GrabWeapon();
            
           

            itemID = weaponData.itemID;
            itemDuration = weaponData.coolDown;
            type = weaponData.type;
            name = weaponData.name;
            speed = weaponData.speed;
            damageOutput = weaponData.damageOutput;
            spriteID = weaponData.sprite;
            sprite = weaponData.sprites;

            ItemFunctions(itemID);
        }

        else
        {
            Debug.Log("cant find");
        }

       


        //var Weapon = 

        Destroy(collision.gameObject);
       
    }



    void ItemFunctions(int theID)
    {
        // put in switch case for various ID

       if (theID >= 1000 && theID <= 2000 )  //update player HP
       {
            if (type == "health")
            {
                hp += itemValue;
                Debug.Log("healed");

            }
            else if (type == "speed") //update player speed
            {
                StartCoroutine(HandleSpeedPotion(itemValue, itemDuration));
                Debug.Log("speed enabled");
            }
       }

       if (theID >= 3000 && theID <= 4000)
        {
            MeleeWeaponTest weaponHolder = meleeWeaponHolder.GetComponent<MeleeWeaponTest>();

            weaponHolder.SwapWeapon(theID); // call theMeleeWeaponTest script to update the SO attached to the holder
            GameObject weaponToInstanitiate = meleeWeaponHolder.gameObject;

            Instantiate(weaponToInstanitiate, weaponPivot.transform); //spawn weapon to player pos

        }
        else if (theID >= 4000 && theID <= 5000)
        {
            ProjectileTemplate projectile = Projectile.GetComponent<ProjectileTemplate>();

            projectile.SwapProjectile(theID);

            GameObject projectileToIntantiate = projectileHolder.gameObject;

            Instantiate(projectileToIntantiate, weaponPivot.transform);

        }


    }

    void SwapSprite(int charID)
    {
        foreach (PlayerTemplate chars in charList)
        {
            if (chars.characterID == charID)
            {
                characters = chars;

                SpriteRenderer spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();

                spriteRenderer.sprite = chars.sprite;
                Debug.Log("here");
            }
        }
    }

   
}
