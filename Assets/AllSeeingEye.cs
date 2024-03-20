using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllSeeingEye : MonoBehaviour
{
    public float timeBeforeAttack = 3.0f;
    public float attackDamage = 10.0f;
    
    private float eyeHeightRange = 1.8f;
    private float dirX = 0.0f;
    
    private float eyeContactCount = 0.0f;

    private Transform PlayerTransform;                                          // stores the player's transform for convenient access.
    private Rigidbody2D playerRb;
    private bool atHeight = false;
    private bool inSight = false;
    private bool eyeContact = false;




    // Start is called before the first frame update
    void Start()
    {
        GameObject p = GameObject.FindWithTag("Player");
        PlayerTransform = p.transform;
        playerRb = p.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float vel = playerRb.velocity.x;
        if (vel < 0) dirX = -1.0f;
        else if (vel > 0) dirX = 1.0f;
    }

    void FixedUpdate()
    {
        atHeight = (PlayerTransform.position.y < transform.position.y + (eyeHeightRange / 2)
                    && PlayerTransform.position.y > transform.position.y - (eyeHeightRange / 2));

        Vector2 firePoint = new Vector2(transform.position.x, transform.position.y - (eyeHeightRange / 2));
        Vector2 direction = new Vector2(-1, 0); 

        RaycastHit2D hit = Physics2D.Raycast(firePoint, direction, Mathf.Infinity, LayerMask.GetMask("Default"));

        if (hit.collider != null && atHeight)
        {
            inSight = (hit.collider.gameObject.name == "Player");
            eyeContact = (dirX == direction.x * -1);
        }
        else inSight = false;

        if (inSight && eyeContact) eyeContactCount += Time.deltaTime;
        else eyeContactCount = 0.0f;

        if (eyeContactCount >= timeBeforeAttack)
        {
            Attack();
            eyeContactCount = 0.0f;
        }
    }

    void Attack()
    {
        PlayerTransform.gameObject.GetComponent<Player>().LoseHealth(attackDamage);
        Debug.Log("Player hit by eye monster!");
        inSight = false;
    }
}
