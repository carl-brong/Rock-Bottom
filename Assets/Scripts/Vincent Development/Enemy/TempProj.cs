using System.Collections;
using UnityEngine;

public class TempProj : MonoBehaviour
{
    private Rigidbody2D rb;
    public float power;
    public Vector2 dir;
    
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
}
