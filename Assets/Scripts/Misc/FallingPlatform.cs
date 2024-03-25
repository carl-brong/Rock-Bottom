using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Carl Brong 
//Falling Platforms
public class FallingPlatform : MonoBehaviour
{
    [SerializeField] private float fallDelayTime;
    [SerializeField] private float delayDestroy;

    private bool isFalling = false;
    [SerializeField] private Rigidbody2D rb;

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
    private IEnumerator Fall()
    {
        isFalling = true;
        yield return new WaitForSeconds(fallDelayTime);

        rb.bodyType = RigidbodyType2D.Dynamic;
        Destroy(gameObject, delayDestroy);
    }
}
