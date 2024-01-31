using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor.Rendering;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    [Header("Movement")]
    [SerializeField] private float accelTime;
    [SerializeField] private float deccelTime;
    [SerializeField] private float maxSpeed;


    [Header("Jump")]
    [SerializeField] private float jumpHeight;
    [SerializeField][Range(0, 1)] private float coyoteTime;
    [SerializeField][Range(0, 1)] private float jumpBuffer;
    [SerializeField] private float gravityScale;
    [SerializeField] private float terminalSpeed;

    private float accelForce;
    private float deccelForce;
    private float move_x;
    private float coyoteTimeCounter;
    private float jumpBufferCounter;

    private bool isGrappling;
    private bool isJumping;
    private bool isJumpFalling;

    private GrapplingHook grapplingHook;

    // Start is called before the first frame update
    void Start()
    {
        rb.gravityScale = gravityScale;

        grapplingHook = GetComponent<GrapplingHook>();
        grapplingHook.enabled = false;
        isGrappling = false;
    }

    // Update is called once per frame
    void Update()
    {
        move_x = Input.GetAxisRaw("Horizontal");
        coyoteTimeCounter -= Time.deltaTime;
        jumpBufferCounter -= Time.deltaTime;

        Jump();

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            grapplingHook.enabled = true;
            isGrappling = true;
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            grapplingHook.enabled = false;
            isGrappling = false;
        }
        
    }

    private void FixedUpdate()
    {
        if (isGrappling && !OnGround())
        {
            grapplingHook.LockRope();
        }
        else
        {
            grapplingHook.UnlockRope();
        }
        Run();
        
    }

    private void Run()
    {
        float targetSpeed = maxSpeed * move_x;
        float force;

        // Acceleration in air and on the ground
        if (!OnGround())
        {
            force = (Mathf.Abs(targetSpeed) > 0.01f) ? accelForce : deccelForce;
        }
        else
        {
            force = (Mathf.Abs(targetSpeed) > 0.01f) ? accelForce : 4 * deccelForce;
        }
        
        // Direction and Power multiplier
        float speedDiff = targetSpeed - rb.velocity.x;

        // Applied Force
        rb.velocity = new Vector2(rb.velocity.x + (Time.fixedDeltaTime * force * speedDiff), rb.velocity.y);

    }


    private bool OnGround()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void OnValidate()
    {
        accelForce = maxSpeed / accelTime;
        deccelForce = maxSpeed / deccelTime;
    }

    private void Jump()
    {
        if (OnGround())
        {
            coyoteTimeCounter = coyoteTime;
            rb.gravityScale = gravityScale;
            isJumpFalling = false;
        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpBufferCounter = jumpBuffer;
        }

        // Apply jump force within grace timers
        if (CanJump() && jumpBufferCounter > 0f)
        {
            isJumping = true;
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
            coyoteTimeCounter = 0f;
            jumpBufferCounter = 0f;
        }

        // Reduce force if the user release the jump key early
        if (Input.GetKeyUp(KeyCode.Space) && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        // Terminal velocity on descent
        if (rb.velocity.y < -terminalSpeed)
        {
            rb.velocity = new Vector2(rb.velocity.x, -terminalSpeed);
        }

        if (isJumping && rb.velocity.y < 0f)
        {
            isJumping = false;
            isJumpFalling = true;
        }

        if (isJumpFalling)
        {
            rb.gravityScale = gravityScale * 2.5f;
        }

    }

    private bool CanJump()
    {
        return coyoteTimeCounter > 0f && !isJumping;
    }

    // Flip player in the direction of mouse
    private void MouseLook()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 localScale = transform.localScale;
        if (mousePos.x < transform.position.x)
        {
            localScale.x = -1;
        }
        else
        {
            localScale.x = 1;
        }
        transform.localScale = localScale;
    }

    // Collision Checks
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Hazard"))
        {
            Debug.Log("Hit Hazard");
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Safe"))
        {
            Debug.Log("Hit Safe");
        }
    }
}
