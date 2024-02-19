using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Projectile : MonoBehaviour
{

    private bool keepAlive;
    public float maxTimeOffScreen = 3f;
    private float counter;
    
    private void OnBecameInvisible()
    {
        keepAlive = false;
    }

    private void OnBecameVisible()
    {
        keepAlive = true;
    }

    private void Start()
    {
        counter = maxTimeOffScreen;
    }

    private void Update()
    {
        if (!keepAlive)
        {
            counter -= Time.deltaTime;
            if (counter < 0)
            {
                Debug.Log("Destroy Projectile");
                Destroy(gameObject);
                counter = maxTimeOffScreen;
            }
        }
    }
}
