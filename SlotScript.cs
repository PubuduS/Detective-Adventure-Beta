using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;



public class SlotScript : MonoBehaviour, IPointerClickHandler, IClickable {

    private ObservableStack<Item> items = new ObservableStack<Item>();


    [SerializeField]
    private Image icon;

    [SerializeField]
    private Text stackSize;

    public bool IsEmpty {

        get
        {
            return items.Count == 0;
        }


    }

    public Item MyItem {

        get
        {
            if (!IsEmpty) {
                return items.Peek();
            }//end of if

            return null;
        }

    }

    public bool IsFull {

        get {

            if (IsEmpty || MyCount < MyItem.MyStackSize) {
                return false;
            }
            return true;  
        }

    }


    public Image MyIcon
    {
        get
        {
            return icon;
        }

        set
        {
            icon = value;
        }
    }

    public int MyCount {

        get { return items.Count;  }

    }// end of MyCount function

    public Text MyStackText
    {
        get
        {
            return stackSize;
        }
    }

    private void Awake()
    {
        items.OnPop += new UpdateStackEvent(UpdateSlot);
        items.OnPush += new UpdateStackEvent(UpdateSlot);
        items.OnClear += new UpdateStackEvent(UpdateSlot);

    }//end of Awake function

    public bool AddItem(Item item) {

        items.Push(item);
        icon.sprite = item.MyIcon;
        icon.color = Color.white;
        item.MySlot = this;

        return true;
    }//end of AddItem function

    public bool AddItems(ObservableStack<Item> newItems) {

        if (IsEmpty || newItems.Peek().GetType() == MyItem.GetType()) { //check for empty slot or items are same type

            int count = newItems.Count;  //if the above condition matches set the count of items

            for (int i = 0; i < count; i++) // take all the item one by one and put it in the new slot
            {
                if (IsFull) {
                    return false;
                }//end of if inside for loop

                AddItem(newItems.Pop());
            }//end of for
            return true;
        }//end of if 
        return false;

    }//end of the function Additems

    public void RemoveItem(Item item) {

        if (!IsEmpty) {
            items.Pop();
           
        }
    }//end of RemoveItem function

    public void Clear() {

        if (items.Count > 0) {
            items.Clear();
        }

    }//end of Clear function

    public void OnPointerClick(PointerEventData eventData)
    {

        if (eventData.button == PointerEventData.InputButton.Left) {

            if (InventoryScript.MyInstance.FromSlot == null && !IsEmpty) {// If we don't have something to move

                HandScript.MyInstance.TakeMoveable(MyItem as IMoveable); //MyItem is the item in actual slot
                InventoryScript.MyInstance.FromSlot = this; //from slot is == to what ever I click

            } else if (InventoryScript.MyInstance.FromSlot != null) { // If we have something to move

                if (PutItemBack() || MergeItems(InventoryScript.MyInstance.FromSlot) ||SwapItems(InventoryScript.MyInstance.FromSlot)  || AddItems(InventoryScript.MyInstance.FromSlot.items) ) {
                    HandScript.MyInstance.Drop();
                    InventoryScript.MyInstance.FromSlot = null;
                }
            }

        }//end of 1st if

        if (eventData.button == PointerEventData.InputButton.Right) {
            UseItem();
        } //end of 2nd if

    }// end of OnPointerClick function

    public void UseItem() {

        if (MyItem is IUseable) {
            (MyItem as IUseable).Use();
        }

    }//end of UseItem function

    public bool StackItem(Item item) {

        if (!IsEmpty && item.name == MyItem.name && items.Count < MyItem.MyStackSize) {
            items.Push(item);
            item.MySlot = this;
            return true;

        }

        return false;


    }//end of the StackItem function

    private bool PutItemBack() {

        if (InventoryScript.MyInstance.FromSlot == this) {
            InventoryScript.MyInstance.FromSlot.MyIcon.color = Color.white;
            return true;
        }
        return false;
    }//end of PutItemBack function

    private bool SwapItems(SlotScript from) {

        if (IsEmpty) {
            return false;
        }

        if (from.MyItem.GetType() != MyItem.GetType() || from.MyCount + MyCount > MyItem.MyStackSize ) {

            //copy all items need to be swap from slot A
            ObservableStack<Item> tmpFrom = new ObservableStack<Item>(from.items);

            //Clear the slot A
            from.items.Clear();

            //Take everything from slot B and copy it into A
            from.AddItems(items);

            //Clear B
            items.Clear();

            //Move from A to B
            AddItems(tmpFrom);
            return true;

        }
        return false;
    }// end of SwapItems function


    private bool MergeItems(SlotScript from) {

        if (IsEmpty) {
            return false;
        }

        if (from.MyItem.GetType() == MyItem.GetType() && !IsFull) {

            //calculate the free slot
            int free = MyItem.MyStackSize - MyCount;

            for (int i = 0; i < free; i++)
            {
                AddItem(from.items.Pop());
            }//end of the for loop

            return true;
        }//end of the second if
        return false;
    }//end of MergeItems function

    private void UpdateSlot() {

        UIManager.MyInstance.UpdateStackSize(this);

    }//end of UpdateSlot function

}//end of SlotScript class 
