using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Vincent Lee
// 5/2/24

public class CheckpointTrigger : MonoBehaviour
{
    private Player _player;
    private bool _triggered;
    private Collider2D collider;
    
    private void Awake()
    {
        _player = GameObject.FindWithTag("Player").GetComponent<Player>();
        _triggered = false;
        collider = GetComponent<Collider2D>();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !_triggered)
        {
            _player.startpos = new Vector3(transform.position.x + collider.offset.x, transform.position.y + collider.offset.y + 0.5f);
            _triggered = true;
            Debug.Log(_player.startpos);
        }
    }
}
