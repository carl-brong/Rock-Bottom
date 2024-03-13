using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using UnityEditor.Rendering;


public class RopeControl : MonoBehaviour
{
    public float swingForce = 5.0f;                                            // used as the strength of player's horizontal movement on a rope.
    public float jumpOffForce = 16.0f;                                          // used as the strength of the vertical jump velocity upon jumping off a rope.

    public static Transform CollidedRope;                                       // stores the transform of the collided section of rope (The endlink in the current implementation)
    public static List<Transform> Ropes;                                        // a list that stores the transforms for the connected rope sections.

    private Transform PlayerTransform;                                          // stores the players transform.
    private int chainIndex;                                                     // stores the current index in the chain of rope sections.
    private Collider2D[] colliders;                                             // an array used to store the player's colliders while they are attached to a rope.
    private Player playerController;                                            // stores a variable for the playerController class for easy access.

    private bool onRope = false;                                                // boolean flag indicating the player is on a rope.
    public bool testJump = false;                                               // public boolean to be used in a unit test.
    public float dirX;                                                          // stores the most recent button press in the horizontal direction.
    private Vector3 scale;                                                      // vector3 for storing the player transform's scale which is used to restore the character's scale upon exiting a rope.



    // Start is called before the first frame update
    void Start()
    {
        PlayerTransform = transform;
        colliders = GetComponentsInChildren<Collider2D>();
        playerController = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (onRope)
        {
            PlayerTransform.position = CollidedRope.position;
            PlayerTransform.localRotation = Quaternion.AngleAxis(0, Vector3.forward);

            if (Input.GetKeyDown(KeyCode.Space) || testJump)
            {
                StartCoroutine(JumpOff());
                PlayerTransform.localRotation = Quaternion.AngleAxis(0, Vector3.forward);
                return;
            }

            dirX = Input.GetAxis("Horizontal");

            //CollidedRope.localScale = scale;
            float angle = 0.0f;
            Vector3 axis = Vector3.zero;
            CollidedRope.rotation.ToAngleAxis(out angle, out axis);
            angle *= Mathf.PI / 180;
            angle = Mathf.Sqrt(angle * angle);
            int i = -1;

            if (angle <= Mathf.PI / 3)                                                                        // if 
                CollidedRope.GetComponent<Rigidbody2D>().AddForce(Vector2.right * dirX * swingForce);
            else if (angle >= Mathf.PI / 2)
                CollidedRope.GetComponent<Rigidbody2D>().velocity = Vector2.down;
        }

    }

    public IEnumerator JumpOff()
    {
        playerController.enabled = true;
        PlayerTransform.parent = null;

        playerController.GetComponent<Rigidbody2D>().rotation = 0.0f;
        playerController.GetComponent<Rigidbody2D>().velocity = Vector2.up * jumpOffForce * 10;
        
        PlayerTransform.localScale = scale;

        CollidedRope.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        onRope = false; 

        yield return new WaitForSeconds(0.25f);

        playerController.GetComponent<Rigidbody2D>().velocity = Vector2.up * jumpOffForce * jumpOffForce;

        foreach (var c in colliders)
            c.enabled = true;

    }

    void attachToRope(Transform colTran)
    {
        scale = PlayerTransform.localScale;
        playerController.GetComponent<Rigidbody2D>().rotation = 0.0f;

        playerController.enabled = false;

        foreach (var c in colliders)
            c.enabled = false;

        var RopesParents = colTran.parent;
        Ropes = new List<Transform>();

        foreach (Transform child in RopesParents)
            Ropes.Add(child);

        CollidedRope = colTran;
        chainIndex = Ropes.IndexOf(CollidedRope);

        PlayerTransform.parent = CollidedRope;
        onRope = true;
    }

    IEnumerator OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Rope2D")
        {
            attachToRope(col.transform);
        }
        yield return null;
    }

    public bool OnRope()
    {
        return onRope;
    }
}