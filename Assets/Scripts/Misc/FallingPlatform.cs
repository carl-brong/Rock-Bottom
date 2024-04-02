using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Rendering.PostProcessing;
using UnityEngine;
//Carl Brong 
//Falling Platforms
public class FallingPlatform : MonoBehaviour
{
    [SerializeField] private float fallDelayTime;
    private float resetTime = 5f;
    //[SerializeField] private float delayDestroy;

    private bool isFalling = false;
    [SerializeField] private Rigidbody2D rb;
    Vector2 defaultPos;

    private void Start()
    {
        defaultPos = transform.position;
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isFalling)
        {
            return;
        }
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(Fall());
        }
    }
    /*private void Update()
    {
        if (Restarter.RestartGame`)
        {

        }
    }*/
    private IEnumerator Fall()
    {
        isFalling = true;
        yield return new WaitForSeconds(fallDelayTime);
        
        rb.bodyType = RigidbodyType2D.Dynamic;

        yield return new WaitForSeconds(resetTime);
        //.Destroy(gameObject, delayDestroy);
        Reset();
        isFalling = false;
    }

    private void Reset()
    {
        rb.bodyType = RigidbodyType2D.Static;
        transform.position = defaultPos;
    }
}
