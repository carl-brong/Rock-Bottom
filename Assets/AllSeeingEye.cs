using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllSeeingEye : MonoBehaviour
{
    private float eyeHeightRange = 1.8f;
    private float playerRadius = 0.7f;

    private Collider2D playerCol;                                               // stores the player's collider object for convenient access.
    private Transform PlayerTransform;                                          // stores the player's transform for convenient access.
    private bool inRange = false;
    private bool inSight = false;
    private bool first = true;

    // Start is called before the first frame update
    void Start()
    {
        GameObject p = GameObject.FindWithTag("Player");
        PlayerTransform = p.transform;
        playerCol = p.GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float curPlayerY = PlayerTransform.position.y;
        if (curPlayerY < transform.position.y && curPlayerY > transform.position.y - eyeHeightRange)
        {
            inRange = true;
            Vector2 firePoint = new Vector2(transform.position.x, transform.position.y - eyeHeightRange / 2);
            RaycastHit2D cast = Physics2D.Raycast(firePoint, Vector2.left);

            if (cast.point.y <= PlayerTransform.position.y && cast.point.y >= PlayerTransform.position.y - eyeHeightRange)
            {
                inSight = true;
                if (cast.collider.name == "Player") // not working
                    PlayerTransform.position = new Vector2(PlayerTransform.position.x, PlayerTransform.position.y + 10);    // test for raycast contact with player

            }

        }
        else if (curPlayerY < (transform.position.y - eyeHeightRange))
            first = true;
    }
}
