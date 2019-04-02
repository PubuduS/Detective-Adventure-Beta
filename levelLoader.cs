using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class levelLoader : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
    public void loadNextScene(){
        int nextScene = SceneManager.GetActiveScene().buildIndex + 1;
        if(nextScene == SceneManager.sceneCountInBuildSettings){
            nextScene = 0;
        }
        SceneManager.LoadScene(nextScene);
    }
}
