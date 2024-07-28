using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.RuleTile.TilingRuleOutput;

//Written by: Ryan Jacob && Tedmund Yap
public class thePlayer : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    PlayerTemplate characters;

    public PlayerTemplate[] charList;

    public GameObject thePlayerObj;

    //component references;
    Rigidbody2D rb;
    ItemPickUp theItem;
    MeleeWeaponTest weapon;
    ProjectileSpawner projectile;
    DefaultWeaponScript defaultProjectile;


    //movement 
    public Vector2 moveDir;

    //variables inherited from SO
    public int characterID;
    public int moveSpeed;
    public int hp;
    public int maxHP;
    public int startingLevel;
    public int maxLevel;
    public int startingExp;
    public int currentExp;
    public int expToLevelUp;
    public float expMultiplier;
    public int playerLevel;


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

    //aim active weapon
    public Vector2 mousePos;
    public Camera sceneCamera;
    public GameObject defaultWeapon;
    public Vector2 saveClicked;
    public bool levelUpActive;
    
    // cooldown for fire
    public float cooldownTimer;
    public float cooldownDuration;
    public bool canFire;


    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
        

        int selectedCharacterIndex = CharacterSelectionManager.SelectedCharacterIndex; //getting selected chracter index from selection manager

        characters = charList[selectedCharacterIndex]; //getting data from selectced character


        

        //re assigning values to player script from SO
        characterID = characters.characterID;
        moveSpeed = (int)characters.movementSpeed;
        hp = characters.health;
        maxHP = characters.maxHealth;
        startingLevel = characters.startingLevel;
        maxLevel = (int)characters.maxLevel;
        startingExp = characters.startingExp;
        expToLevelUp = characters.expToLevelUp;
        expMultiplier = characters.expMultiplier;
        sprite = characters.sprite;

        SwapSprite(characterID);


        defaultProjectile = defaultWeapon.GetComponent<DefaultWeaponScript>();
        canFire = true; //ensureing player can always fire on start of game
        cooldownDuration = 3; //Ensures player isn't able to fire twice at the start of the game
        playerLevel = startingLevel; //ensurs player level is set to default
        
    }

    // Update is called once per frame
    void Update()
    {

        

    }

    private void FixedUpdate()
    {
        Move();
        InputManagement();
    }

    void InputManagement()
    {
        
        xAxis = Input.GetAxisRaw("Horizontal");
        yAxis = Input.GetAxisRaw("Vertical");

        moveDir = new Vector2(xAxis, yAxis).normalized;

        mousePos = sceneCamera.ScreenToWorldPoint(Input.mousePosition); //getting position of the mouse

        if (Input.GetMouseButtonDown(0) && canFire == true)  //checking if button is pressed and cooldown on weapon has expired
        {
            saveClicked = mousePos;// saving mouse click last pos
            FireProjectile(); //fire projectile
            StartCoroutine(weaponCoolDown()); //initiate cooldown coroutine

        }
        else if (Input.GetMouseButtonDown(0) && canFire == false)
        {
            //Debug.Log("Cant Fire now");
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            Debug.Log("M key pressed, adding XP");
            AddToXP(500); // Call the AddToXP method to test leveling up
        }

    }
    public void GivingCoolDown(int coolDown) //getting cooldown from defaultWeaponScript
    {
        cooldownDuration = coolDown; // assigning data from weapon
    }

    private IEnumerator weaponCoolDown() //cool down method
    {
        canFire = false;
        //Debug.Log("Weapon cooldown started");
        yield return new WaitForSeconds(cooldownDuration); 
        canFire = true;
        //Debug.Log("Weapon cooldown ended");
    }

    void FireProjectile() 
    {

        //Debug.Log("Firing projectile");

        GameObject projectileInstance = Instantiate(defaultWeapon, transform.position, Quaternion.identity);  // instantiate the projectile at the player's position

        DefaultWeaponScript projectileScript = projectileInstance.GetComponent<DefaultWeaponScript>(); // accessing the DefaultWeaponScript

        projectileScript.Player = this.gameObject; // the player reference
    }


    void Move()
    {
       
        rb.velocity = new Vector2 (moveDir.x * moveSpeed, moveDir.y*moveSpeed);
       
    }

    public void TakeDamage(int atkDamage) //for enemies to call to damage player
    {
        hp -= atkDamage;

        if (hp <= 0)
        {
            //End Game
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

        try
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

                Debug.Log("Item picked up");
            }

            else if (theItem.tag == "Weapon")
            {
                var weaponData = theItem.GrabWeapon();

                itemID = weaponData.itemID;             // reassigning item variables fro item pickup script
                itemDuration = weaponData.coolDown;
                type = weaponData.type;
                name = weaponData.name;
                speed = weaponData.speed;
                damageOutput = weaponData.damageOutput;
                spriteID = weaponData.sprite;
                sprite = weaponData.sprites;

                ItemFunctions(itemID);

                Debug.Log("Weapon picked up");
            }

            else
            {
                Debug.Log("cant find");
            }



            Destroy(collision.gameObject);

        }

        catch { }
       
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

            projectile.SwapProjectile(theID); //swap projectile based on ID

            GameObject projectileToIntantiate = projectileHolder.gameObject; //set the spawn obj

            Instantiate(projectileToIntantiate, weaponPivot.transform);

        }


    }

    void SwapSprite(int charID) //swapping sprite based on character selected in character select screen
    {
        foreach (PlayerTemplate chars in charList) //checking char list
        {
            if (chars.characterID == charID) // if ID matches then swap sprite;
            {
                characters = chars;

                SpriteRenderer spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();

                spriteRenderer.sprite = chars.sprite;
                //Debug.Log("here");
            }
        }
    }

    public void AddToXP(int xpToGet) //to get exp from specific enemies
    {
        currentExp += xpToGet; // add to current exp

        if (currentExp >= expToLevelUp) // if it hits level cap, level up and increment level cap and hp
        {
            startingLevel++; // increase level

            while (currentExp >= expToLevelUp && startingLevel < maxLevel)
            {

                int healthCapIncrement = (int)(maxHP * expMultiplier);  // calculate the new XP and HP increment
                int expIncrement = (int)(expToLevelUp * expMultiplier);

                maxHP += healthCapIncrement; // update player's max HP and current HP
                hp += (int)(healthCapIncrement * expMultiplier);

                expToLevelUp += expIncrement; //update exp to level up

                DefaultWeaponScript defaultWeaponScript = defaultWeapon.GetComponent<DefaultWeaponScript>(); //call level up default weapon method in default weapon script 
                defaultWeaponScript.Player = this.gameObject;
                defaultWeaponScript.LevelUpDefaultWeapon(); //pass to defaultweapon script
            }

        }
    }

}
