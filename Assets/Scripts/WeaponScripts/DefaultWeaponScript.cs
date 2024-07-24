using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.GraphicsBuffer;

public class DefaultWeaponScript : MonoBehaviour
{
    [SerializeField]
    public Weapons defaultWeapon;

    public GameObject Player;
    private thePlayer callingPlayer;

    public GameObject theSprite;

    // attributes;
    public int damage;
    public int speed;
    public int defaultCooldown;
    public Sprite assignedSprite;

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



        isLevelingUp = false;
        // assigning the variables
        damage = defaultWeapon.damageOutput;
        speed = defaultWeapon.speed;
        defaultCooldown = defaultWeapon.coolDown;
        assignedSprite = defaultWeapon.sprites;

        // accessing player data
        callingPlayer = Player.GetComponent<thePlayer>();

        targetPos = callingPlayer.saveClicked; // Set target position from player
        playerLevel = callingPlayer.playerLevel; // getting player level

        // Calculate the direction to move towards the target position
        theDirection = (targetPos - (Vector2)transform.position).normalized;

        // Accessing the projectile's sprite renderer
        SpriteRenderer spriteRenderer = theSprite.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = assignedSprite; // Swap sprite

        InitiateCoolDown(defaultCooldown);

        
    }

    // Update is called once per frame
    void Update()
    {
        if (isLevelingUp == true)
        {
            ApplyUpgrade();
            transform.position += (Vector3)theDirection * speed * Time.deltaTime;
            isLevelingUp = false; // Prevent continuous upgrades
        }
        else
        {
            transform.position += (Vector3)theDirection * speed * Time.deltaTime;
        }
    }

    public void InitiateCoolDown(int coolDown) // sending cooldown to player
    {
       
        coolDown = defaultCooldown;
        callingPlayer.GivingCoolDown(coolDown);
        
    }

    public void LevelUpDefaultWeapon()
    {  
        thePlayer thePlayerStats = Player.GetComponent<thePlayer>();

        float coolDownIncrement = (defaultCooldown * thePlayerStats.expMultiplier); // calculating stats to upgrade
        float damageIncrement = (int)(damage * thePlayerStats.expMultiplier);
        float speedIncrement = (int)(speed * thePlayerStats.expMultiplier);

        DefaultWeaponManager theManager = Player.GetComponent<DefaultWeaponManager>(); //calling manager to store data


        theManager.UpdateWeaponStats(damageIncrement, speedIncrement, coolDownIncrement);
        isLevelingUp = true;

       
    }

    public void GetIncrements(int damageInc, int speedInc, int cooldownInc)
    {
       dmgInc = damageInc;
       spInc = speedInc;
       cdInc = cooldownInc;
    }

    public void ApplyUpgrade()
    {
        Debug.Log("Applying Upgrades");
        damage += dmgInc;
        speed += spInc;
        defaultCooldown += cdInc;

        // Update sprite color to be more red
        if (theSprite != null)
        {
            SpriteRenderer spriteRenderer = theSprite.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                Color currentColor = spriteRenderer.color;
                currentColor.r = Mathf.Clamp(currentColor.r + 0.1f, 0, 1); // increase red component
                spriteRenderer.color = currentColor;
            }
            else
            {
                Debug.LogError("SpriteRenderer component is missing on theSprite.");
            }
        }

        isLevelingUp = false;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("mapBounds"))
        {
            Destroy(gameObject);
        }
    }
}
