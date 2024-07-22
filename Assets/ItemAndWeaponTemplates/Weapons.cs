using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Melee Weapon", menuName = "Weapon/MeleeWeapon")] // tells unity what to display options as
public class Weapons : ScriptableObject //inherit scriptable object
{
    // public Sprite image;
    public int weaponID;
    public string itemName;
    public int speed;
    public int damageOutput;
    public int coolDown;
    public string type;
    public Sprite sprites;
    public int spriteID;
   
}
