using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackFollower : MonoBehaviour
{
    [SerializeField] public float acceleration = 5.0f;
    [SerializeField] public float maxSpeed = 50.0f;


    void Start()
    {

    }

    void Update()
    {
        GameObject p = GameObject.Find("Player");
        if (p.transform.IsChildOf(transform))
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.right * 2);
            //float currentSpeed = Time.deltaTime * acceleration;
            //if (currentSpeed < maxSpeed)
            //    GetComponent<Rigidbody2D>().AddForce(Vector2.right * acceleration);
            //else
           //     GetComponent<Rigidbody2D>().AddForce(Vector2.right * maxSpeed);
        }
    }
}
