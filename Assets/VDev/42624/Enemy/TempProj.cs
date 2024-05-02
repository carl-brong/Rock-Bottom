using System;
using System.Collections;
using UnityEngine;

// Vincent Lee
// 5/2/24

public class TempProj : MonoBehaviour
{
    private Rigidbody2D rb;
    public float power;
    public Vector2 dir;
    public static event Action<float> PlayerHit;
    
    // Start is called before the first frame update

    private void Start()
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
        rb.velocity = dir * power;
    }

    private IEnumerator LifeTime()
    {
        yield return new WaitForSeconds(2f);
        ObjectPoolManager.ReturnObjectToPool(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerHit?.Invoke(1);
            StopCoroutine(LifeTime());
            ObjectPoolManager.ReturnObjectToPool(gameObject);
        }

        else if (other.gameObject.layer == 6)
        {
            StopCoroutine(LifeTime());
            ObjectPoolManager.ReturnObjectToPool(gameObject);
        }
    }
}
