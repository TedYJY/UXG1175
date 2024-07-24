using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    [SerializeField]
    private Potions potions;

    [SerializeField]
    private Weapons weapons;

    // variables for standard items
    public int itemID;
    public int itemTimer;
    public int duration;
    public int value;
    public string type;

    //additional variables for weapons
    public int speed;
    public int coolDown;
    public int sprite;
    public int damageOutput;
    Sprite sprites;
    

    thePlayer player;
    // Start is called before the first frame update
    

    // try to incorporate id just to read the so and send ID back to player

    public (int itemID, int itemTimer, int value, string type ) GrabItemValues() 
    {

        // for standard items
        itemID = this.potions.itemID;
        itemTimer = this.potions.duration;
        value = this.potions.value;
        type = this.potions.type;

        return (itemID, itemTimer, value, type);


    }

    public (int itemID, string name, string type, int speed, int damageOutput, int coolDown, int sprite, Sprite sprites) GrabWeapon()
    {
        if (weapons == null)
        {
            Debug.Log("weapon not found");
        }

        int itemID = this.weapons.weaponID;
        string name = this.weapons.itemName;
        string type = this.weapons.type;
        int speed = this.weapons.speed;
        int damageOutput = this.weapons.damageOutput;
        int coolDown = this.weapons.coolDown;
        int sprite = this.weapons.spriteID;
        sprites = this.weapons.sprites;




        return (itemID, name, type, speed, damageOutput, coolDown, sprite, sprites);
    }

    
       

   

}
    