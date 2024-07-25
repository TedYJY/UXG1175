using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Written by Ryan Jacob
public class MeleeWeaponHandler : MonoBehaviour
{
    [SerializeField]
    Weapons weapon;

    Enemy theEnemy;

    // Update is called once per frame
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>().TakeDamage(weapon.damageOutput);
        }
    }
}
