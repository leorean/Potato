using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject followingObject;
    
    float xVel;
    float yVel;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (followingObject == null) return;

        float dist = Vector2.Distance(transform.position, followingObject.transform.position);
        if (dist < 0.1f) return;

        xVel = ((followingObject.transform.position.x - transform.position.x) / 64);
        yVel = ((followingObject.transform.position.y - transform.position.y) / 32);

        transform.position = new Vector3(transform.position.x + xVel, transform.position.y + yVel, transform.position.z);
        

    }    
}
