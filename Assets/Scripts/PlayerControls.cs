using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(Rigidbody2D))]


public class PlayerControls : MonoBehaviour
{
    public float walkSpeed = 5f;
    Vector2 moveInput;
    [SerializeField] int jump;

    public ProjectileBehavior projectileBehavior;
    public Transform launchOffset;
    public static PlayerControls instance;

    public bool IsMoving { get; private set; }
    Rigidbody2D rb;

    [SerializeField] Transform groundCheckCollider;
    [SerializeField] bool onGround = false;
    const float GCRad = 0.2f;
    [SerializeField] LayerMask groundLayer;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //only has one player per scene
        //instance = this;
        /*if (instance != null )
        {
            Destroy(this.gameObject);
            //return;
        }
        */
       //GameObject.DontDestroyOnLoad(this.gameObject);
        //GameObject.DontDestroyOnLoad(rb.position.x);
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(rb.position.x + "," + rb.position.y);
        groundCheck();
        //move l / r
        float horizInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput.x * walkSpeed, rb.velocity.y);

        //rotate l / r
        if (!Mathf.Approximately(0, horizInput))
        {
            transform.rotation = horizInput < 0 ? UnityEngine.Quaternion.Euler(0, 180, 0) : UnityEngine.Quaternion.identity;
        }
        //jump

        if (Input.GetButtonDown("Jump") && onGround)
        {
            rb.velocity = new Vector2(rb.velocity.x, jump);
        }

        if (Input.GetKeyDown("e"))
        {
            Instantiate(projectileBehavior, launchOffset.position, transform.rotation);
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

        IsMoving = moveInput != Vector2.zero;
    }

    void groundCheck()
    {
        onGround = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckCollider.position, GCRad, groundLayer);
        if (colliders.Length > 0)
        {
            onGround = true;
        }
    }
}
