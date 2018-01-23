using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public string jumpKey = "up";
    public string leftKey = "left";
    public string rightKey = "right";

    public float jumpSpeed = 5f;
    public float rotationSpeed = 11f;
    public float horizontalJumpAcceleration = .15f;
    public float maxVelocityX = 4f;
    public float maxJumpVelocityX = 3f;
    public float maxVelocityY = 5f;

    /* for mass = 1
    public float jumpSpeed = 5f;
    public float rotationSpeed = 4.2f;
    public float acceleration = .05f;
    public float maxVelocityX = 4f;
    public float maxJumpVelocityX = 2.5f;
    public float maxVelocityY = 5f;*/

    Animator animator;

    public enum Direction
    {
        Left = -1,
        Right = 1
    }
    public enum State
    {
        Idle = 0,
        Jump = 1,
        Hurt = 2,
        Win = 3
    }
    public State state = State.Idle;
    public Direction direction = Direction.Right;
    
    bool isJumpPressing;
    bool isLeftPressing;
    bool isRightPressing;
    bool isOnGround;
    bool isAgainstSides;
    bool canJump;

    Rigidbody2D rb2d;
    PhysicsMaterial2D material;
    float friction;
    float scaleX;
    float scaleY;
    float angularDrag;
    
    // Use this for initialization
    void Start () {

        rb2d = gameObject.GetComponent<Rigidbody2D>();
        scaleX = transform.localScale.x;
        scaleY = transform.localScale.y;
        angularDrag = rb2d.angularDrag;
        animator = gameObject.GetComponent<Animator>();
        material = rb2d.sharedMaterial;
        friction = material.friction;
    }

    // Update is called once per frame
    void Update () {

        isJumpPressing = Input.GetKey(jumpKey);
        isLeftPressing = Input.GetKey(leftKey);
        isRightPressing = Input.GetKey(rightKey);
        
        // flip player when facing other direction
        transform.localScale = new Vector3(scaleX * (int)direction, scaleY);
        
        animator.SetInteger("State", (int)state);
    }
    
    void FixedUpdate()
    {
        rb2d.velocity = new Vector2(Mathf.Sign(rb2d.velocity.x) * Mathf.Min(Mathf.Abs(rb2d.velocity.x), maxVelocityX),
            Mathf.Sign(rb2d.velocity.y) * Mathf.Min(Mathf.Abs(rb2d.velocity.y), maxVelocityY));

        isOnGround = IsOnGround();
        //isAgainstSides = IsAgainstSides();
        if (!canJump && isOnGround)
            canJump = true;
        if (rb2d.velocity.y < -2)
            canJump = false;
        
        if (state == State.Idle && !isOnGround)
            state = State.Jump;

        if (state == State.Jump && isOnGround)
            state = State.Idle;

        if (isOnGround)
        {
            material.friction = friction;
            rb2d.sharedMaterial = material;
        }
        else
        {
            material.friction = 0;
            rb2d.sharedMaterial = material;
        }

        if (isJumpPressing)
        {
            if (canJump)
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, jumpSpeed);
                canJump = false;
            }
        }

        float rot = (float)(rotationSpeed + Convert.ToInt32(isOnGround) * rotationSpeed * .25);

        if (isLeftPressing)
        {            
            rb2d.AddTorque(rot);
            if (rb2d.velocity.x < 0.1f) direction = Direction.Left;

            if (!isOnGround)
            {
                if (rb2d.velocity.x > -maxJumpVelocityX)
                    rb2d.velocity = new Vector2(rb2d.velocity.x - horizontalJumpAcceleration, rb2d.velocity.y);
            }
        }
        if (isRightPressing)
        {
            rb2d.AddTorque(-rot);
            if (rb2d.velocity.x > 0.1f) direction = Direction.Right;
            if (!isOnGround)
            {
                if (rb2d.velocity.x < maxJumpVelocityX)                    
                    rb2d.velocity = new Vector2(rb2d.velocity.x + horizontalJumpAcceleration, rb2d.velocity.y);
            }
        }
    }
    
    //////

    bool IsAgainstSides()
    {
        var cf = new ContactFilter2D();
        cf.SetLayerMask(LayerMask.GetMask("BlockLayer"));
        var pc = GetComponent<PolygonCollider2D>();
        int h = 0;
        foreach (Vector2 pcv in pc.points)
        {
            Vector3 pcv2world = transform.TransformPoint(pcv);
            var color = Color.red;
            if (pcv2world.y > transform.position.y - pc.bounds.size.y * .3
                && pcv2world.y < transform.position.y + pc.bounds.size.y * .3)
            {
                color = Color.yellow;
                h += Physics2D.Raycast(pcv2world, Vector2.left, cf, new RaycastHit2D[1], 0.1f);
                h += Physics2D.Raycast(pcv2world, Vector2.right, cf, new RaycastHit2D[1], 0.1f);
            }
            //Debug.DrawRay(pcv2world, Vector2.left * 0.1f, color);
            //Debug.DrawRay(pcv2world, Vector2.right * 0.1f, color);
        }
        return h > 0;
    }

    bool IsOnGround()
    {
        var cf = new ContactFilter2D();
        cf.SetLayerMask(LayerMask.GetMask("BlockLayer"));
        var pc = GetComponent<PolygonCollider2D>();
        int h = 0;
        foreach (Vector2 pcv in pc.points)
        {
            Vector3 pcv2world = transform.TransformPoint(pcv);
            //Debug.DrawRay(pcv2world, Vector2.down* 0.05f, Color.blue);
            h += Physics2D.Raycast(pcv2world, Vector2.down, cf, new RaycastHit2D[1], 0.05f);
        }        
        return h > 0;
    }    
}
