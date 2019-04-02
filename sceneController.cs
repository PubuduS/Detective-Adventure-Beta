using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sceneController : MonoBehaviour {

    void Awake()
    {
        int sceneControllerCount = FindObjectsOfType<sceneController>().Length;
        if(sceneControllerCount > 1){
            gameObject.SetActive(false);
            Destroy(gameObject);
        }else{
            DontDestroyOnLoad(gameObject);
        }
    }

}
