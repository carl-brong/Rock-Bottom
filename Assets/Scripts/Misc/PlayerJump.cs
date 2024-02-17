using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField]  int jump;

    public Transform groundcheck;
    public LayerMask groundLayer;
    bool onGround;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        onGround = Physics2D.OverlapCapsule(groundcheck.position, new Vector2(-0.01128709f, -0.9622799f), CapsuleDirection2D.Horizontal, 0, groundLayer);
        if (Input.GetButtonDown("Jump") && onGround)
        {
            rb.velocity = new Vector2(rb.velocity.x, jump);
        }
    }
}
