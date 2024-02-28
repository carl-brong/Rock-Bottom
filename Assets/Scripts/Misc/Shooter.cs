using UnityEngine;

public class Shooter : MonoBehaviour
{

    public float interval = 3f;
    private float counter;
    public GameObject ammo;

    void Start()
    {
        counter = interval;
    }

    private void FixedUpdate()
    {
        Shoot();
    }

    private void Shoot()
    {
        counter -= Time.deltaTime;
        if (counter < 0)
        {
            GameObject projectile = Instantiate(ammo, transform.position, transform.rotation);
            projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(transform.localScale.x * 2f, 0f);
            counter = interval;
        }
    }
}
