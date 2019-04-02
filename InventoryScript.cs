using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryScript : MonoBehaviour {


    private static InventoryScript instance;

    public static InventoryScript MyInstance    
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<InventoryScript>();
            }

            return instance;
        }
    }

    private SlotScript fromSlot;

    private List<Bag> bags = new List<Bag>();

    [SerializeField]
    private BagButton[] bagButtons;

    [SerializeField]
    private Item[] items;


    public bool CanAddBag {
        get { return bags.Count < 5; }
    }

    public SlotScript FromSlot
    {
        get
        {
            return fromSlot;
        }

        set
        {
            fromSlot = value;

            if (value != null) {
                fromSlot.MyIcon.color = Color.grey;
            }
            fromSlot = value;
        }
    }

    private void Awake()
    {
        Bag bag = (Bag)Instantiate(items[0]);
        bag.Initialize(20);
        bag.Use();
    }//end of Awake function

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.J)) {
            Bag bag = (Bag)Instantiate(items[0]);
            bag.Initialize(20);
            bag.Use();
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            Bag bag = (Bag)Instantiate(items[0]);
            bag.Initialize(20);
            AddItem(bag);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            HealthPotion potion = (HealthPotion)Instantiate(items[1]);
            AddItem(potion);
        }


    }//end of the Update function

    public void AddBag(Bag bag) {

        foreach (BagButton bagButton in bagButtons)
        {
            if (bagButton.MyBag == null) {
                bagButton.MyBag = bag;
                bags.Add(bag);
                break;
            }
        }//end of foreach loop

    }//end of AddBag function

    public void AddItem(Item item) {

        if (item.MyStackSize > 0) {

            if (PlaceInStack(item)) {
                return;
            }
        }

        PlaceInEmpty(item);
    

    }//end of AddItem function

    private void PlaceInEmpty(Item item) {

        foreach (Bag bag in bags)
        {

            if (bag.MyBagScript.AddItem(item)) {
                return;
            }

        }//end of outer foreach loop

    }//end of PlaceInEmpty function

    private bool PlaceInStack(Item item) {

        foreach (Bag bag in bags)
        {

            foreach (SlotScript slots in bag.MyBagScript.MySlots)
            {

                if (slots.StackItem(item)) {
                    return true;

                }//end of if


            }//end of inner foreach loop


        }//end of outer foreach loop

        return false;
    }//end of the PlaceInStack function

    public void OpenClose() {

        bool closedBag = bags.Find(x => !x.MyBagScript.IsOpen);

        //if closed bag true, open all closed bag
        //if closed bag false, close all open bag
        foreach (Bag bag in bags)
        {
            if (bag.MyBagScript.IsOpen != closedBag) {
                bag.MyBagScript.OpenClose();
            }
        }//end of foreach loop

    }//end of OpenClose function

}//end of InventoryScript class
