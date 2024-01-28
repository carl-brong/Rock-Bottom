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

    [Header("Metrics")]
    [SerializeField] private float accelTime;
    [SerializeField] private float deccelTime;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float jumpHeight;
    [SerializeField] private float coyoteTime;
    [SerializeField] private float jumpBuffer;
    [SerializeField] private float gravityScale;

    private float accelForce;
    private float deccelForce;
    private float move_x;
    private float coyoteTimeCounter;
    private float jumpBufferCounter;
    public bool isGrappling;

    // Start is called before the first frame update
    void Start()
    {
        rb.gravityScale = gravityScale;
        isGrappling = false;
    }

    // Update is called once per frame
    void Update()
    {
        move_x = Input.GetAxisRaw("Horizontal");


        if (OnGround())
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpBufferCounter = jumpBuffer;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }


        if (coyoteTimeCounter > 0f && jumpBufferCounter > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
            coyoteTimeCounter = 0f;
            jumpBufferCounter = 0f;
        }
        if (Input.GetKeyUp(KeyCode.Space) && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

    }

    private void FixedUpdate()
    {
        if (!isGrappling || OnGround())
        {
            Run();
        }
        
    }

    private void Run()
    {
        float targetSpeed = maxSpeed * move_x;
        float force = (Mathf.Abs(targetSpeed) > 0.01f) ? accelForce : deccelForce;
        float speedDiff = targetSpeed - rb.velocity.x;

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


}
