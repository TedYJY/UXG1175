using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class weaponPickUp : MonoBehaviour
{
    [SerializeField]
    private Potions potions;
    

    thePlayer player;
    // Start is called before the first frame update
     public void RemoveItem()
    {
        gameObject.SetActive(false);
    }

    public int HealingPotion()
    {
        int toHeal = potions.value;
        gameObject.SetActive(false);
        return toHeal;
    }



   

}
