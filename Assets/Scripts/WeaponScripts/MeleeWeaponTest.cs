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

    public GameObject theSprite;
    public bool isAccessed = false;

    public void SwapWeapon(int weaponID) // called from item player script
    {
       foreach (Weapons weapon in meleeWeaponsList)  // sort through list
        {
          if (weapon.weaponID == weaponID) //compare ID
            {
                weapons = weapon; // assign the weapon So when item is picked up based on ID

               
               SpriteRenderer spriteRenderer = theSprite.GetComponent<SpriteRenderer>();

               spriteRenderer.sprite = weapon.sprites;

            }
        
        }

        isAccessed = false;
    }

    

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler (0,0, transform.rotation.eulerAngles.z + (weapons.speed * Time.deltaTime)); //rotating the melee weapon
    
    }

}
