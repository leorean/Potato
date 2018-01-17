using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public string jumpKey = "up";
    public string leftKey = "left";
    public string rightKey = "right";

    public float jumpSpeed = 4f;
    public float rotationSpeed = 3.8f;
    public float horizontalSpeed = 2f;
    public float maxVelocityX = 4f;
    public float maxVelocityY = 5f;

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
    float angularDrag;
    
    // Use this for initialization
    void Start () {

        rb2d = gameObject.GetComponent<Rigidbody2D>();
        scaleX = transform.localScale.x;
        scaleY = transform.localScale.y;
        angularDrag = rb2d.angularDrag;
    }

    // Update is called once per frame
    void Update () {

        isJumpPressed = Input.GetKey(jumpKey);
        isLeftPressing = Input.GetKey(leftKey);
        isRightPressing = Input.GetKey(rightKey);

        // flip player when facing other direction
        transform.localScale = new Vector3(scaleX * (int)direction, scaleY);
    }
    
    void FixedUpdate()
    {
        rb2d.velocity = new Vector2(Mathf.Sign(rb2d.velocity.x) * Mathf.Min(Mathf.Abs(rb2d.velocity.x), maxVelocityX),
            Mathf.Sign(rb2d.velocity.y) * Mathf.Min(Mathf.Abs(rb2d.velocity.y), maxVelocityY));

        if (IsOnGround())
            isAlreadyJumped = false;

        if (IsOnGround())
        {
            rb2d.angularDrag = angularDrag;
        } else
        {
            rb2d.angularDrag = 0;
        }

        if (isJumpPressed)
        {
            if (!isAlreadyJumped)
            {
                isAlreadyJumped = true;
                rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
                rb2d.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
            }
        }
        
        if (isLeftPressing)
        {
            //rb2d.AddTorque(rotationSpeed);
            rb2d.MoveRotation(rb2d.rotation + rotationSpeed);            
            if (rb2d.velocity.x < 0.1f) direction = Direction.Left;

            if (!IsOnGround())
            {
                rb2d.AddForce(Vector2.left * horizontalSpeed);
            }
        }
        if (isRightPressing)
        {
            //rb2d.AddTorque(-rotationSpeed);
            rb2d.MoveRotation(rb2d.rotation - rotationSpeed);            
            if (rb2d.velocity.x > 0.1f) direction = Direction.Right;
            if (!IsOnGround())
            {
                rb2d.AddForce(Vector2.right * horizontalSpeed);
            }
        }
    }

    //////

    bool IsOnGround()
    {

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.5f, LayerMask.GetMask("BlockLayer"));
        /*if (hit.collider != null)
            print("HIT: " + hit.collider.name);*/
        return (hit.collider != null);
    }
}
