﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour {
    public float speed = 20f;
    public Rigidbody2D bulletRB; 
	// Use this for initialization
	void Start () {
          bulletRB.velocity = transform.right * speed;
		
	}

    private void OnTriggerEnter2D(Collider2D collision){

        if(collision.gameObject.tag == "mummy"){

        }
        
    }


}
