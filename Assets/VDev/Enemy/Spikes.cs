using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Vincent Lee
// 5/2/24

public class Spikes : MonoBehaviour
{
    public static event Action<float> HitSpike;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            HitSpike?.Invoke(100);
        }
    }
}
