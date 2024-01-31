using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Burst.CompilerServices;
using UnityEditor.SceneTemplate;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.HID;


// Grapple Mechanic modified and tuned from below resources
// https://www.youtube.com/watch?v=Gx46xUgVXrQ
// https://www.youtube.com/watch?v=dnNCVcVS6uw

public class GrapplingHook : MonoBehaviour
{
    [SerializeField] private float maxGrappleLength;
    [SerializeField] private float grappleTime;
    [SerializeField] private float reelSpeed;
    [SerializeField] private LayerMask grappleLayer;
    
    private DistanceJoint2D pivot;
    private LineRenderer rope;
    private bool isGrappling;
    private float elapsedTime;
    
    private Vector2 mousePos;

    private void OnEnable()
    {
        // Initialize components
        pivot = GetComponent<DistanceJoint2D>();
        pivot.autoConfigureDistance = true;
        pivot.enableCollision = true;
        pivot.maxDistanceOnly = true;
        pivot.enabled = false;

        rope = GetComponent<LineRenderer>();
        rope.enabled = false;
        elapsedTime = 0f;


        // Set a valid joint pivot located at the mouse and setup rope renderer.
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, Mathf.Infinity, grappleLayer);
        if (hit.collider != null)
        {
            pivot.connectedAnchor = hit.point;
            rope.enabled = true;
            isGrappling = true;
        }
    }

    private void Update()
    {

        // Render the rope gradually with lerp.
        if (isGrappling)
        {
            rope.SetPosition(0, transform.position);
            elapsedTime += Time.deltaTime;
            Vector2 step = Vector2.Lerp(transform.position, pivot.connectedAnchor, elapsedTime / grappleTime);
            rope.SetPosition(1, step);
            
            // Once at the end, attach the player to pivot and restart timer.
            if ((Vector2)rope.GetPosition(1) == pivot.connectedAnchor)
            {
                isGrappling = false;
                pivot.enabled = true;
                elapsedTime = 0f;
                
            }
        }

        // Continually update rope position to match player position
        if (rope.enabled)
        {
            rope.SetPosition(0, transform.position);
        }
    }

    private void FixedUpdate()
    {
        // Reeling
        if (Input.GetKey(KeyCode.W))
        {
            pivot.distance = Mathf.Max(2, pivot.distance - reelSpeed);
        }

        if (Input.GetKey(KeyCode.S))
        {
            pivot.distance = Mathf.Min(pivot.distance + reelSpeed, maxGrappleLength);
        }
    }

    private void OnDisable()
    {
        // Reset components before disable
        if (pivot != null && rope != null)
        {
            rope.SetPosition(1, transform.position);
            isGrappling = false;
            pivot.enabled = false;
            rope.enabled = false;
        }
    }

    public void LockRope()
    {
        pivot.maxDistanceOnly = false;
    }

    public void UnlockRope()
    {
        pivot.maxDistanceOnly = true;
    }
}
