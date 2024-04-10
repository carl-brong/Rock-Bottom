using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Carl Brong
public class EnemyDamage : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    // Start is called before the first frame update
    [SerializeField] private float damage;
    private float timer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Health playerHealth = collision.GetComponent<Health>();
        timer -= Time.deltaTime;
        if (playerHealth != null && timer < 0f)
        {
            playerHealth.takeDamage(damage);
            timer = 1f;
            //rb.velocity = new Vector2(rb.velocity.y, 5);
            //Invoke("OnTriggerEnter2D", 0.5f);//WaitForSeconds(0.1f);
        }
    }

}
