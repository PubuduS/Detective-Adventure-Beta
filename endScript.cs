using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endScript : MonoBehaviour {

    private levelLoader levelLoadScript;

	// Use this for initialization
	void Start () {
        levelLoadScript =
            GameObject.Find("sceneController").GetComponent<levelLoader>();
		
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Entered");
        if(other.gameObject.tag == "Player"){
            levelLoadScript.loadNextScene();
        }
        
    }
}
