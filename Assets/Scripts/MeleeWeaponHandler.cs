using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeaponHandler : MonoBehaviour
{
    [SerializeField]
    MeleeWeapons weapon;

    Enemy theEnemy;

    // Update is called once per frame
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy theEnemy = collision.GetComponent<Enemy>();
            Debug.Log($"Dealing {weapon.damageOutput} damage to enemy");
            theEnemy.TakeDamage(weapon.damageOutput);
        }
    }
}
