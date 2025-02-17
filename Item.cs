﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : ScriptableObject, IMoveable {

    [SerializeField]
    private Sprite icon;

    [SerializeField]
    private int stackSize;

    private SlotScript slot;

    public Sprite MyIcon
    {
        get
        {
            return icon;
        }
       
    }

    public int MyStackSize
    {
        get
        {
            return stackSize;
        }

      
    }

    public  SlotScript MySlot
    {
        get
        {
            return slot;
        }

        set
        {
            slot = value;
        }
    }

    public void Remove() {

        if (MySlot != null) {
            MySlot.RemoveItem(this);
        }

    }//end of Remove function

}//end of the Item class
