using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Health Potion", menuName = "Potion/HealthPotion")] // tells unity what to display options as

public class Potions : ScriptableObject
{
    public string itemName;
    public string type;
    public int value;
}
