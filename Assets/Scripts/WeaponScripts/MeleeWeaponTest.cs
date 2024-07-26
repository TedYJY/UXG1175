using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

//Written by Ryan Jacob
public class MeleeWeaponTest : MonoBehaviour
{
    // Start is called before the first frame update


    [SerializeField]
    public Weapons weapons; // for scriptable object to be assigned

    public Weapons[] meleeWeaponsList; // array of all possible weapons

    public GameObject theSprite; // sprite attached to the actual weapon object

    MeleeWeaponHandler weaponHandler; //actual weapon damage;

    public bool isAccessed = false;

    public int thisWeaponDamage;

    public void SwapWeapon(int weaponID) // called from item player script
    {
       foreach (Weapons weapon in meleeWeaponsList)  // sort through list
        {
          if (weapon.weaponID == weaponID) //compare ID
            {
                weapons = weapon; // assign the weapon So when item is picked up based on ID

                thisWeaponDamage = weapons.damageOutput;
               
               SpriteRenderer spriteRenderer = theSprite.GetComponent<SpriteRenderer>();

               spriteRenderer.sprite = weapon.sprites;

                PassWeaponDamage(thisWeaponDamage);

            }
        
        }

        isAccessed = false;
    }

    

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler (0,0, transform.rotation.eulerAngles.z + (weapons.speed * Time.deltaTime)); //rotating the melee weapon
    
    }

    public void PassWeaponDamage(int weaponDamage)
    {
        MeleeWeaponHandler theWeapon = theSprite.GetComponent<MeleeWeaponHandler>();

        theWeapon.GetDamage(weaponDamage);
    }

}
