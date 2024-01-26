using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(Rigidbody2D))]


public class PlayerControls : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    [Header("Movement Metrics")]
    [SerializeField] private float maxHorizontalSpeed;
    [SerializeField] private float horizontalAcceleration;
    [SerializeField] private float jumpHeight;
    [SerializeField] private float jumpBuffer;
    [SerializeField] private float coyoteTime;

    private float horizontalSpeed = 0f;
    private float direction;
    private float jumpBufferCounter;
    private float coyoteTimeCounter;
    public bool IsMoving { get; private set; }


    // Update is called once per frame
    void Update()
    {
        direction = Input.GetAxisRaw("Horizontal");

        //Horizontal Movement w/ Acceleration
        if (direction != 0 && horizontalSpeed < maxHorizontalSpeed)
        {
            horizontalSpeed += horizontalAcceleration * Time.deltaTime;   
        }
        if (direction == 0 && horizontalSpeed > 0)
        {
            horizontalSpeed = 0f;
        }
        //Coyote Time
        if (IsGrounded())
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        //Jump Buffer
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpBufferCounter = jumpBuffer;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        //Jumping
        if (jumpBufferCounter > 0f && coyoteTimeCounter > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
        }
        if (Input.GetKeyUp(KeyCode.Space) && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(direction * horizontalSpeed, rb.velocity.y);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }
}
