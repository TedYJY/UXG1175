using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class MeleeWeaponTest : MonoBehaviour
{
    // Start is called before the first frame update



   /* public string itemName;
    public int rotationSpeed;
    public int damage;
    public Sprite theSprite;*/

    MeleeWeapons meleeWeapons;
    EnemyMovement theEnemy;

    [SerializeField]
    private MeleeWeapons weapon;

    void Start()
    {
       /* this.rotationSpeed = meleeWeapons.rotationSpeed;
        this. itemName = meleeWeapons.itemName;
        this.damage = meleeWeapons.damageOutput;
        this.theSprite = meleeWeapons.sprite;*/
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler (0,0, transform.rotation.eulerAngles.z + (weapon.rotationSpeed * Time.deltaTime)); //rotating the melee weapon
    }

}
