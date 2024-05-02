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
    
    private void Awake()
    {
        _player = GameObject.FindWithTag("Player").GetComponent<Player>();
        _triggered = false;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !_triggered)
        {
            _player.startpos = other.transform.position;
            _triggered = true;
            Debug.Log(_player.startpos);
        }
    }
}
