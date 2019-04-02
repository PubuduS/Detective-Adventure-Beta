using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followScript : MonoBehaviour {
    [SerializeField]
    private Transform target;
    [SerializeField]
    private Vector3 offset;
    [SerializeField]
    private float smoothSpeed = 0.125f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothPosition = Vector3.Lerp(transform.position, 
                                              desiredPosition,
                                              smoothSpeed);
        transform.position = smoothPosition;
        transform.LookAt(target);

	}
}
