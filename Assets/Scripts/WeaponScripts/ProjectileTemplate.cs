using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileTemplate : MonoBehaviour
{
    [SerializeField]
    public Weapons projWeaponToSwap;

    public GameObject theSprite;

    public Weapons[] projectileWeaponList;

    public Vector2 target;

    public void SwapProjectile(int projWeaponID)
    {
        foreach (var projWeapon in projectileWeaponList)
        {

            Debug.Log("swappingProjectile");
            if (projWeaponID == projWeapon.weaponID)  //swapping attributes
            {
                SpriteRenderer spriteRenderer = theSprite.GetComponent<SpriteRenderer>();
                projWeaponToSwap = projWeapon;
                spriteRenderer.sprite = projWeapon.sprites;
               
            }
        }
    }


    public void SetTarget(Vector2 TargetPosition)
    {
        target = TargetPosition; //takes targets from projectile spawnerscript
       
    }
  
    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, projWeaponToSwap.speed * Time.deltaTime); //moves projectile towards object

        if (Vector2.Distance(transform.position, target) < 0.1f) //destroying object
        {
            Destroy(gameObject);
        }
    }
}
