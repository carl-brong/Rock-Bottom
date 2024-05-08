using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using UnityEditor.Rendering;


public class RopeControl : MonoBehaviour
{
    public float swingForce = 8.0f;                                            // used as the strength of player's horizontal movement on a rope.
    public float jumpOffForce = 2.5f;                                          // used as the strength of the vertical jump velocity upon jumping off a rope.

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
    private bool swingable = false;                                             // swinging is disabled at this time, it's too buggy to deal with at the moment.


    // Start is called before the first frame update
    void Start()
    {
        PlayerTransform = transform;
        colliders = GetComponentsInChildren<Collider2D>();
        playerController = GetComponent<Player>();
        scale = PlayerTransform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (onRope)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine(JumpOff());
                PlayerTransform.localScale = scale;
                return;
            }

            PlayerTransform.position = new Vector2(PlayerTransform.position.x, CollidedRope.position.y);
            PlayerTransform.localRotation = Quaternion.AngleAxis(0, Vector3.forward);

            if (swingable)                                                                                      // swinging is disabled for this build, but this is the code.
            {
                float angle = 0.0f;
                Vector3 axis = Vector3.zero;
                CollidedRope.rotation.ToAngleAxis(out angle, out axis);                                         // get current angle of the section of rope the player is attached to.
                angle *= Mathf.PI / 180;                                                                        // convert degrees to radians.
                angle = Mathf.Sqrt(angle * angle);                                                              // finding absolute value.

                dirX = Input.GetAxis("Horizontal");                                                             // get current horizontal input.

                if (angle <= Mathf.PI / 3)                                                                      // if the rope's angle is less than pi/3, allow player to add a left or right force.
                    CollidedRope.GetComponent<Rigidbody2D>().AddForce(Vector2.right * dirX * swingForce);
                else if (angle >= Mathf.PI / 2)                                                                 // stopping the rope from swinging passed pi/2 (or 90 degrees).
                    CollidedRope.GetComponent<Rigidbody2D>().velocity = Vector2.down;
            }
            
        }

    }

    public IEnumerator JumpOff()
    {
        playerController.enabled = true;
        PlayerTransform.parent = null;

        PlayerTransform.localRotation = Quaternion.AngleAxis(0, Vector3.forward);

        float tDirY = Input.GetAxis("Vertical");                                                             // get current vertical input.

        if (tDirY > 0)                                                                                      // if the player isn't pressing 'down', an upward velocity is applied to the player.
            playerController.GetComponent<Rigidbody2D>().velocity = Vector2.up * jumpOffForce * 10;
        
        CollidedRope.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        onRope = false; 

        yield return new WaitForSeconds(0.25f);

        foreach (var c in colliders)
            c.enabled = true;

        DontDestroyOnLoad(PlayerTransform.gameObject);
    }

    void attachToRope(Transform colTran)
    {
        playerController.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        PlayerTransform.localRotation = Quaternion.AngleAxis(0, Vector3.forward);

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

        CollidedRope.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
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