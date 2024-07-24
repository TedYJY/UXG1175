using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public void UpdateWeaponStats(float floatDamageIncrement, float floatSpeedIncrement, float floatCooldownIncrement)
    {

        DefaultWeaponScript theMainDefaultWeapon = weapon.GetComponent<DefaultWeaponScript>();

        int coolDownIncrement = Mathf.Max(1, Mathf.RoundToInt(floatCooldownIncrement)); 
        int damageIncrement = Mathf.Max(1, Mathf.RoundToInt(floatDamageIncrement)); 
        int speedIncrement = Mathf.Max(1, Mathf.RoundToInt(floatSpeedIncrement));


        weaponDamage += damageIncrement;
        weaponSpeed += speedIncrement;
        weaponCooldown += coolDownIncrement;

        theMainDefaultWeapon.GetIncrements(weaponDamage,weaponSpeed,weaponCooldown);
    }

   /* public void SpawnNewWeapon()
    {
        DefaultWeaponScript weaponScript = defaultWeaponPrefab.GetComponent<DefaultWeaponScript>();

        weaponScript.damage = weaponScript.damage + weaponDamage;
        weaponScript.speed = weaponScript.speed + weaponSpeed;
        weaponScript.defaultCooldown = weaponScript.defaultCooldown + weaponCooldown;
       
        Instantiate(defaultWeaponPrefab, transform.position, Quaternion.identity);
        
    }*/

    


}
