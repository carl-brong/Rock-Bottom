using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    // Start is called before the first frame update
    [SerializeField] private float damage;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Health playerHealth = collision.GetComponent<Health>();
        if (playerHealth != null)
        {
            playerHealth.takeDamage(damage);
            //rb.velocity = new Vector2(rb.velocity.y, 5);
            //Invoke("OnTriggerEnter2D", 0.5f);//WaitForSeconds(0.1f);
        }
    }

}
