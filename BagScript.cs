using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagScript : MonoBehaviour {

    [SerializeField]
    private GameObject slotPrefab;

    private CanvasGroup canvasGroup;

    private List<SlotScript> slots = new List<SlotScript>();

    public bool IsOpen {

        get {

            return canvasGroup.alpha > 0;

        }
    }// end of IsOpen property

    public List<SlotScript> MySlots
    {
        get
        {
            return slots;
        }

       
    }

    private void Awake()
    {

        canvasGroup = GetComponent<CanvasGroup>();


    }//end of Awake function

    public void AddSlots(int slotCount) {

        for (int i=0; i<slotCount; i++) {

           SlotScript slot =    Instantiate(slotPrefab, transform).GetComponent<SlotScript>();
            MySlots.Add(slot);

        }//end of for loop

    }//end of AddSlots function

    public bool AddItem(Item item) {

        foreach (SlotScript slot in MySlots)
        {
            if (slot.IsEmpty) {
                slot.AddItem(item);
                return true;
            }//end of if

        }//end of foreach loop
        return false;
    }//end of AddItem function


    public void OpenClose() {

        canvasGroup.alpha = canvasGroup.alpha > 0 ? 0 : 1;
        canvasGroup.blocksRaycasts = canvasGroup.blocksRaycasts == true ? false : true;

    }//end of OpenClose function


}//end of BagScript class
