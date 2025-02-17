﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager:MonoBehaviour {

    private static UIManager instance;

    public static UIManager MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<UIManager>();
            }

            return instance;
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.B)) {
            InventoryScript.MyInstance.OpenClose();
        }
	}

    public void UpdateStackSize(IClickable clickable) {

        if (clickable.MyCount > 1)
        {
            clickable.MyStackText.text = clickable.MyCount.ToString();
            clickable.MyStackText.color = Color.white;
            clickable.MyIcon.color = Color.white;
        }
        else {

            clickable.MyStackText.color = new Color(0, 0, 0, 0);
            clickable.MyIcon.color = Color.white;
        }


        if (clickable.MyCount == 0) {
            clickable.MyIcon.color = new Color(0,0,0,0);
            clickable.MyStackText.color = new Color(0, 0, 0, 0);
        }

    }//end of the UpdateStackSize function
}
