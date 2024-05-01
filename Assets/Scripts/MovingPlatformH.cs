using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformH : MonoBehaviour                // 'H' stands for 'horizontal' (this script moves a platform in the x-dimension.
{
    [SerializeField] public float speed = 6.0f;
    [SerializeField] public float maxDistance = 5.0f;
    private bool cartActive = false;
    private bool playerOnCart = false;
    private float distanceTraveled = 0.0f;

    void Update()
    {
        GameObject p = GameObject.Find("Player");
        float distance = Time.deltaTime * speed;

        if (cartActive)
        {
            float xPlatformPos = GetComponent<Rigidbody2D>().position.x;
            float yPlatformPos = GetComponent<Rigidbody2D>().position.y;
            GetComponent<Rigidbody2D>().position = new Vector2(xPlatformPos + distance, yPlatformPos);

            distanceTraveled += distance;
            if (distanceTraveled >= maxDistance)
            {
                cartActive = false;
                playerOnCart = false;
            }

            if (playerOnCart)
            {
                float xPlayerPos = p.GetComponent<Rigidbody2D>().transform.position.x;
                float yPlayerPos = p.GetComponent<Rigidbody2D>().transform.position.y;
                p.GetComponent<Rigidbody2D>().transform.position = new Vector2(xPlayerPos + distance, yPlayerPos);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "Player")
        {
            transform.GetComponent<Rigidbody2D>().AddForce(col.gameObject.GetComponent<Rigidbody2D>().totalForce);
            cartActive = true;
            playerOnCart = true;
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.name == "Player")
        {
            playerOnCart = false;
        }
    }
}