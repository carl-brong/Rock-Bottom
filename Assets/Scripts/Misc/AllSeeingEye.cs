using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllSeeingEye : MonoBehaviour
{
    public float timeBeforeAttack = 3.0f;                                       // the amount of time the player can look at the eye monster before being hurt by it.
    public float attackDamage = 10.0f;                                          // the attack power of the eye monster.
    public float eyeHeightRange = 1.8f;                                         // the height of the eye monster in world coordinates. Value can be changed.
    public int lookingDirection = -1;                                           // this variable determines the direction the eye will be looking, either left (-1) or right (1); do not set to any value other than -1 or 1.
    
    
    private float dirX = 0.0f;                                                  // this variable is used to store the direction which the player is currently facing.
    
    private float eyeContactCount = 0.0f;                                       // stores the length of time the player has been making eye contact with the eye monster

    private Transform PlayerTransform;                                          // stores the player's transform for convenient access.
    private Rigidbody2D playerRb;                                               // stores the player's rigidbody component.
    
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
        float vel = playerRb.velocity.x;                            // get player's velocity.
        if (vel < 0) dirX = -1.0f;                                  // set direction player is facing by getting the current or most recent velocity of the player.
        else if (vel > 0) dirX = 1.0f;
    }

    void FixedUpdate()
    {
        atHeight = (PlayerTransform.position.y < transform.position.y + (eyeHeightRange / 2)                        // set a boolean flag; true if the player is within range of the eye in the y-dimension.
                    && PlayerTransform.position.y > transform.position.y - (eyeHeightRange / 2));

        Vector2 firePoint = new Vector2(transform.position.x, transform.position.y - (eyeHeightRange / 2));         // store vector to represent the point of origin for the raycast.
        Vector2 direction = new Vector2(lookingDirection, 0);                                                       // vector storing the direction that the eye will be looking.

        RaycastHit2D hit = Physics2D.Raycast(firePoint, direction, Mathf.Infinity, LayerMask.GetMask("Default"));   // perform a raycast using the two stored vectors, store the result in a 'RaycastHit2D' object.

        if (hit.collider != null && atHeight)                                           // check if the raycast hit something and if the player is within the y-dimension range of the eye.
        {
            inSight = (hit.collider.gameObject.name == "Player");                       // sets the boolean flag with the result of the raycast from the eye; true if it collides with the player.
            eyeContact = (dirX == direction.x * -1);                                    // for eye contact to occur, the player and the eye must be facing opposite directions (facing eachother); sets a boolean flag
        }
        else inSight = false;                                                           // if player is out of the eye's line of sight, set false

        if (inSight && eyeContact)
        {
            eyeContactCount += Time.deltaTime;
            Debug.Log("In");
        }// if the player and the eye are making eye contact, increment the timer.
        else eyeContactCount = 0.0f;                                                    // if eye contact is broken at any time, reset the timer.

        if (eyeContactCount >= timeBeforeAttack)                                        // once the player is making eye contact for long enough, the attack function is called and the timer is reset.
        {
            Attack();
            eyeContactCount = 0.0f;
        }
    }

    void Attack()
    {
        PlayerTransform.gameObject.GetComponent<Player>().LoseHealth(attackDamage);             // take health from the player
        Debug.Log("Player hit by eye monster!");
        inSight = false;                                                                        
    }
}
