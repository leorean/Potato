using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrPlayer : MonoBehaviour {

    bool keyJump;
    bool keyLeft;
    bool keyRight;
    Rigidbody2D rb2d;

	// Use this for initialization
	void Start () {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {

        keyJump = Input.GetKeyDown("up");
        keyLeft = Input.GetKey("left");
        keyRight = Input.GetKey("right");

        if (keyJump)
            print("jump was pressed");
    }


    void FixedUpdate()
    {
        /*float h = Input.GetAxis("Horizontal") * 10 * Time.deltaTime;
        float v = Input.GetAxis("Vertical") * 10 * Time.deltaTime;*/

        if (keyLeft)
            rb2d.MoveRotation(rb2d.rotation + 5);
        if (keyRight)
            rb2d.MoveRotation(rb2d.rotation - 5);        
    }
}
