using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempProj : MonoBehaviour
{
    private Rigidbody2D rb;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        StartCoroutine(LifeTime());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        rb.velocity = Vector2.right * 8;
    }

    private IEnumerator LifeTime()
    {
        yield return new WaitForSeconds(2.1f);
        ObjectPoolManager.ReturnObjectToPool(gameObject);
    }
}
