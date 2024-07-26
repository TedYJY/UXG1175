using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Written by Ryan Jacob
public class MeleeWeaponHandler : MonoBehaviour
{
    [SerializeField]
    Weapons weapon;

    public int weaponDamage;

    Enemy theEnemy;

    // Update is called once per frame
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>().TakeDamage(weaponDamage); //pass to enemy script to deal damage
        }
    }

    public void GetDamage(int damage)
    {
        weaponDamage = damage;
    }
}
