using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.GraphicsBuffer;

//Written by Ryan Jacob
public class DefaultWeaponScript : MonoBehaviour
{
    [SerializeField]
    public Weapons defaultWeapon;

    public GameObject Player;
    private thePlayer callingPlayer;

    public GameObject theSprite;

    // attributes
    public int damage;
    public int speed;
    public int defaultCooldown;
    public Sprite assignedSprite;
    private int maxHits;

    public int startingDamage;
    public int startingSpeed;
    public int startingCooldown;


    public int dmgInc;
    public int spInc;
    public int cdInc;

    Vector2 targetPos;
    Vector2 theDirection;

    public int playerLevel;
    public bool isLevelingUp;

    // Start is called before the first frame update
    void Start()
    {
        
        //ResetInc(); // abit jank

        isLevelingUp = false;

        // Assign initial values from ScriptableObject
        startingDamage = defaultWeapon.damageOutput;
        startingSpeed = defaultWeapon.speed;
        startingCooldown = defaultWeapon.coolDown;
        assignedSprite = defaultWeapon.sprites;

        // data to be updated
        damage = startingDamage;
        speed = startingSpeed;
        defaultCooldown = startingCooldown;

        // Accessing player data
        callingPlayer = Player.GetComponent<thePlayer>();

        targetPos = callingPlayer.saveClicked; // Set target position from player
        playerLevel = callingPlayer.playerLevel; // Get player level

        // calculate the direction to move towards the target position
        theDirection = (targetPos - (Vector2)transform.position).normalized;

        // access the projectile's sprite renderer and assign the sprite
        SpriteRenderer spriteRenderer = theSprite.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = assignedSprite;

        InitiateCoolDown(defaultCooldown);
    }

    // Update is called once per frame
    void Update()
    {
        playerLevel = callingPlayer.playerLevel;

        if (isLevelingUp)
        {
            //applyUpgrade();
            isLevelingUp = false; // Prevent continuous upgrades
        }

        // Move projectile in the calculated direction
        transform.position += (Vector3)theDirection * (startingSpeed + spInc) * Time.deltaTime;
    }

    public void InitiateCoolDown(int coolDown) // Sending cooldown to player
    {
        callingPlayer.GivingCoolDown(defaultCooldown + cdInc);
    }

    public void LevelUpDefaultWeapon()
    {
        thePlayer thePlayerStats = Player.GetComponent<thePlayer>();

        // Calculating stats to upgrade
        int coolDownIncrement = (int)(defaultCooldown * thePlayerStats.expMultiplier);
        int damageIncrement = (int)(damage * thePlayerStats.expMultiplier);
        int speedIncrement = (int)(speed * thePlayerStats.expMultiplier);

        DefaultWeaponManager theManager = Player.GetComponent<DefaultWeaponManager>(); // Calling manager to store data
        theManager.UpdateWeaponStats(damageIncrement, speedIncrement, coolDownIncrement); // Store data

        GetIncrements(damageIncrement, speedIncrement, coolDownIncrement); // Fetch increments
        isLevelingUp = true; // Trigger upgrade
    }

    public void GetIncrements(int damageInc, int speedInc, int cooldownInc) // Get data to increment
    {
        dmgInc += damageInc;
        spInc += speedInc;
        cdInc += cooldownInc;

        GiveDamage(startingDamage, dmgInc);
    }

    public void GiveDamage(int startingDamage, int dmgInc)
    {
        int dmageToDeal = startingDamage + dmgInc;

        // pass to enemy script;

    }

    public void ResetInc()
    {
        //reset values
        dmgInc = 0;
        spInc = 0;
        cdInc = 0;
    }


    /* public void ApplyUpgrade()
     {
         Debug.Log("Applying Upgrades");
         // Applying the increments
         damage += dmgInc;
         speed += spInc;
         defaultCooldown += cdInc;

         // Access the sprite renderer and update color
         SpriteRenderer spriteRenderer = theSprite.GetComponent<SpriteRenderer>();
         if (spriteRenderer != null)
         {
             Color currentColor = spriteRenderer.color;
             currentColor.r = Mathf.Clamp(currentColor.r + 0.1f, 0, 1); // Increase red component
             spriteRenderer.color = currentColor;
         }
         else
         {
             Debug.LogError("SpriteRenderer component is missing on theSprite.");
         }
     }*/

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("mapBounds"))
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>().TakeDamage(damage + dmgInc); //pass to enemy script to deal damage

            if (maxHits++ >= 4)
            {
                maxHits = 0;
                Destroy(gameObject);
            }
        }
    }

   

}
