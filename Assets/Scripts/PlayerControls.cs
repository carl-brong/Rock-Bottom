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
    [SerializeField] private float speed = 10f;
    [SerializeField] private float terminalVelocity = -20f;
    [SerializeField] private float jumpHeight = 25f;
    [SerializeField] private float jumpBuffer = 0.2f;
    [SerializeField] private float coyoteTime = 0.2f;

    private float direction;
    private float jumpBufferCounter;
    private float coyoteTimeCounter;
    public bool IsMoving { get; private set; }


    // Update is called once per frame
    void Update()
    {
        direction = Input.GetAxisRaw("Horizontal");

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
            jumpBufferCounter = 0f;
            coyoteTimeCounter = 0f;
        }
        if (Input.GetKeyUp(KeyCode.Space) && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(direction * speed, (rb.velocity.y > terminalVelocity) ? rb.velocity.y : terminalVelocity);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

}
