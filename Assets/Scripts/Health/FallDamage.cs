using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDamage : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private Health playerHealth;
    Rigidbody2D rb;


    [SerializeField] Transform groundCheckCollider;
    [SerializeField] bool onGround = false;
    const float GCRad = 0.2f;
    [SerializeField] LayerMask groundLayer;
    public float minFall = 10f;

    bool wasGrounded;
    bool wasFalling;
    float startFall;
    float fallDistance;
    bool isFall { get { return (!onGround && rb.velocity.y < 0); } }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerHealth = player.GetComponent<Health>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        groundCheck();

        if (!wasFalling && isFall)
        {
            startFall = transform.position.y;
        }
        if(!wasGrounded && onGround)
        {
            fallDistance = startFall - transform.position.y;
            if (fallDistance > minFall)
            {
                playerHealth.takeDamage((fallDistance- minFall) * 0.03f);
            }
            else
            {
                playerHealth.takeDamage(0);
            }
            Debug.Log(fallDistance);
        }
            //;
        
        wasGrounded = onGround;
        wasFalling = isFall;
  
            //playerHealth.takeDamage(0.1f);
       
        
    
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