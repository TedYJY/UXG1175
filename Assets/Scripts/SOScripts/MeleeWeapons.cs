using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Written by Ryan Jacob
[CreateAssetMenu(fileName = "New Melee Weapon", menuName = "Weapon/MeleeWeapon")] // tells unity what to display options as
public class MeleeWeapons : ScriptableObject //inherit scriptable object
{
   // public Sprite image;
    public string itemName;
    public int rotationSpeed;
    public int damageOutput;
   
}
