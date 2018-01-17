using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public string jumpKey = "up";
    public string leftKey = "left";
    public string rightKey = "right";

    public float jumpSpeed = 2f;
    public float rotationSpeed = 8f;
    public float horizontalSpeed = 2f;

    public enum Direction
    {
        Left = -1,
        Right = 1
    }
    public Direction direction = Direction.Right;

    bool isJumpPressed;
    bool isLeftPressing;
    bool isRightPressing;

    bool isAlreadyJumped;

    Rigidbody2D rb2d;

    float scaleX;
    float scaleY;

	// Use this for initialization
	void Start () {

        rb2d = gameObject.GetComponent<Rigidbody2D>();
        scaleX = transform.localScale.x;
        scaleY = transform.localScale.y;
    }
	
	// Update is called once per frame
	void Update () {

        isJumpPressed = Input.GetKeyDown(jumpKey);
        isLeftPressing = Input.GetKey(leftKey);
        isRightPressing = Input.GetKey(rightKey);

        transform.localScale = new Vector3(scaleX * (int)direction, scaleY);
    }


    void FixedUpdate()
    {        
        if (isJumpPressed)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
            rb2d.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);

        }

        if (isLeftPressing)
        {
            rb2d.MoveRotation(rb2d.rotation + rotationSpeed);
            rb2d.AddForce(Vector2.left * horizontalSpeed);
            direction = Direction.Left;
        }
        if (isRightPressing)
        {
            rb2d.MoveRotation(rb2d.rotation - rotationSpeed);
            rb2d.AddForce(Vector2.right * horizontalSpeed);
            direction = Direction.Right;
        }
    }
}
