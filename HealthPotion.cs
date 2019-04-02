using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HealthPotion", menuName =  "Items/Potion", order = 1)]
public class HealthPotion : Item, IUseable
{
    [SerializeField]
    private int health;


    public void Use()
    {

       //f (playerController.MyInstance.MyHealth.MyCurrentValue < playerController.MyInstance.MyHealth.MyMaxValue) {

       // }  end of if

        Remove();
        //ayerController.MyInstance.MyHealth.MyCurrentValue += health;

    }
}
