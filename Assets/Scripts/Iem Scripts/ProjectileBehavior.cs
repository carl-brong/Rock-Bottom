using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    public float speed = 40.0f;
    public float damage = 0.1f;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.right * Time.deltaTime * speed;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
           
        if (collision.gameObject.TryGetComponent<EnemyHealth>(out EnemyHealth enemy))
        {
            enemy.takeDamage(damage);
        }


        Destroy(gameObject);

    }
}
