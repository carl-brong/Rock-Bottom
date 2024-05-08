

using System;
using UnityEngine;

// Vincent Lee

public class LampInteractable : MonoBehaviour, IInteractable
{
    public bool held;
    public bool interactable;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Interact(Player player)
    {
        if (!interactable) return;
        if (held)
        {
            transform.parent = GameObject.Find("Unload").transform;
            transform.parent = null;
            rb.isKinematic = false;
        }
        else
        {
            transform.parent = player.transform;
            transform.position = new Vector2(player.transform.position.x, player.transform.position.y + 1f);
            rb.isKinematic = true;
        }
        held = !held;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            interactable = true;
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            interactable = false;
        }
    }
    
}
