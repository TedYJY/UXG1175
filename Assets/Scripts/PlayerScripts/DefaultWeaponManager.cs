using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Written by Ryan Jacob
public class DefaultWeaponManager : MonoBehaviour
{
    public DefaultWeaponManager defaultWeaponManager;
    public GameObject defaultWeaponPrefab;


    public GameObject weapon;
    DefaultWeaponScript theMainDefaultWeapon;

    // Store weapon stats
    public int weaponDamage;
    public int weaponSpeed;
    public int weaponCooldown;


    // Method to update weapon stats

    private void Start()
    {
        //reset database
        weaponDamage = 0;
        weaponSpeed = 0;
        weaponCooldown = 0;
    }

    public void UpdateWeaponStats(float floatDamageIncrement, float floatSpeedIncrement, float floatCooldownIncrement)
    {

        DefaultWeaponScript theMainDefaultWeapon = weapon.GetComponent<DefaultWeaponScript>();

        int coolDownIncrement = Mathf.Max(1, Mathf.RoundToInt(floatCooldownIncrement)); 
        int damageIncrement = Mathf.Max(1, Mathf.RoundToInt(floatDamageIncrement)); 
        int speedIncrement = Mathf.Max(1, Mathf.RoundToInt(floatSpeedIncrement));


        weaponDamage = damageIncrement;
        weaponSpeed = speedIncrement;
        weaponCooldown = coolDownIncrement;

        theMainDefaultWeapon.GetIncrements(weaponDamage,weaponSpeed,weaponCooldown);
    }


    


}
