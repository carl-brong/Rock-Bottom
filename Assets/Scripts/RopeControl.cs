using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using UnityEditor.Rendering;


public class RopeControl : MonoBehaviour
{
    public float swingForce = 3.0f;
    public float jumpOffForce = 8.0f;

    public static Transform CollidedChain;
    public static List<Transform> Chains;

    private Transform PlayerTransform;
    private int chainIndex;
    private Collider2D[] colliders;
    private Player playerController;
    private Quaternion locRotation;

    public bool onRope = false;
    public bool testJump = false;
    public float dirX;
    private Vector3 scale;



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
            PlayerTransform.position = CollidedChain.position;
            PlayerTransform.localRotation = Quaternion.AngleAxis(0.0f, Vector3.forward);

            if (Input.GetKeyDown(KeyCode.Space) || testJump)
            {
                StartCoroutine(JumpOff());

            }

            dirX = Input.GetAxis("Horizontal");

            float dif = Quaternion.Angle(PlayerTransform.localRotation, Quaternion.AngleAxis(0, Vector3.forward));
            if (dif <= 10.0f)
                CollidedChain.GetComponent<Rigidbody2D>().AddForce(Vector2.right * dirX * swingForce);
        }

    }

    public IEnumerator JumpOff()
    {
        PlayerTransform.parent = null;
        onRope = false;
        playerController.enabled = true;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        PlayerTransform.localRotation = locRotation;
        PlayerTransform.localScale = scale;

        playerController.GetComponent<Rigidbody2D>().velocity = new Vector2(jumpOffForce * dirX, jumpOffForce);
        //detachFromRope();
        yield return new WaitForSeconds(0.30f);

        foreach (var c in colliders)
            c.enabled = true;
    }

    void detachFromRope()
    {
        PlayerTransform.parent = null;
        onRope = false;
        playerController.enabled = true;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        PlayerTransform.localRotation = locRotation;
        PlayerTransform.localScale = scale;

        playerController.GetComponent<Rigidbody2D>().velocity = new Vector2(jumpOffForce * dirX, jumpOffForce);
    }

    void attachToRope(Transform colTran)
    {
        scale = PlayerTransform.localScale;

        playerController.enabled = false;

        foreach (var c in colliders)
            c.enabled = false;

        var chainsParents = colTran.parent;
        Chains = new List<Transform>();

        foreach (Transform child in chainsParents)
            Chains.Add(child);

        CollidedChain = colTran;
        chainIndex = Chains.IndexOf(CollidedChain);

        PlayerTransform.parent = CollidedChain;
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
}