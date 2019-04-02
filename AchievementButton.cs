using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementButton : MonoBehaviour {

    public GameObject achivementList;

    public Sprite neutral, highlight;

    private Image sprite;


	// Use this for initialization
	void Awake () {
        sprite = GetComponent<Image>();

	}
	
	// Update is called once per frame
	public void Click () {

        if (sprite.sprite == neutral)
        {
            sprite.sprite = highlight;
            achivementList.SetActive(true);
        }
        else {
            sprite.sprite = neutral;
            achivementList.SetActive(false);
        }
		
	}
}
