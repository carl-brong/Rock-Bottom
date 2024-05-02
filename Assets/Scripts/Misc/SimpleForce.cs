using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleForce : MonoBehaviour
{
    [SerializeField] private float _amount;
    private Rigidbody2D Rb;
    
    // Start is called before the first frame update
    void Start()
    {
        Rb = GetComponent<Rigidbody2D>();
        Rb.velocity = Vector2.down * _amount;
        Rb.rotation = Random.Range(0, 360);
        Rb.angularVelocity = Random.Range(-0.5f, 0.5f);
    }
    
}
