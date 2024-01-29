using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneTemplate;
using UnityEngine;
using UnityEngine.InputSystem;


// Grapple Mechanic modified and tuned from below resources
// https://www.youtube.com/watch?v=Gx46xUgVXrQ
// https://www.youtube.com/watch?v=dnNCVcVS6uw

public class GrapplingHook : MonoBehaviour
{

    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LineRenderer rope;
    [SerializeField] private SpringJoint2D tether;

    public PlayerController player;
    private Rigidbody2D rb;
    private Vector2 grappleAnchor;
    private float grappleTime = 0f;


    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody2D>();
        rope.enabled = false;
        tether.enabled = false;
        tether.autoConfigureDistance = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            player.isGrappling = false;
            grappleTime = 0f;
            SetAnchor();
        }
        else if (Input.GetKey(KeyCode.Mouse0))
        {
            grappleTime += Time.deltaTime;
            if (rope.enabled)
            {
                DrawRope(); 
            }
            SetTether();

        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            rope.enabled = false;
            tether.enabled = false;
            rope.SetPosition(1, transform.position);
            player.isGrappling = false;
        }
    }

    private void SetAnchor()
    {
        Vector2 mousePos = GetMousePosition();
        RaycastHit2D hit = Physics2D.Raycast(mousePos, direction: Vector2.zero, distance: Mathf.Infinity, layerMask: groundLayer);
        Debug.Log(hit.collider);

        if (hit.collider != null)
        {
            grappleAnchor = hit.point;
            rope.enabled = true;
        }
    }

    private Vector2 GetMousePosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void DrawRope()
    {
        rope.SetPosition(0, transform.position);
        float progression = grappleTime / 0.1f;
        Vector2 step = Vector2.Lerp(transform.position, grappleAnchor, progression);
        rope.SetPosition(1, step);
    }

    private void SetTether()
    {
        if ((Vector2)rope.GetPosition(1) == grappleAnchor)
        {
            player.isGrappling = true;
            tether.connectedAnchor = grappleAnchor;
            tether.enabled = true;
        }
    }
}
