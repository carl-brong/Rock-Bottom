using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineCartControl : MonoBehaviour
{
    [SerializeField] public float acceleration = 5.0f;
    [SerializeField] public float maxForce = 1.0f;


    void Start()
    {
        
    }

    void Update()
    {
        GameObject p = GameObject.Find("Player");
        if (p.transform.IsChildOf(transform))
        {
            p.GetComponent<Rigidbody2D>().transform.position = new Vector2(GetComponent<Rigidbody2D>().transform.position.x, p.GetComponent<Rigidbody2D>().transform.position.y);
            p.GetComponent<Rigidbody2D>().transform.localRotation = Quaternion.AngleAxis(0, Vector3.forward);

            float xForce = GetComponent<Rigidbody2D>().totalForce.x;
            if (xForce < maxForce)
                GetComponent<Rigidbody2D>().AddForce(Vector2.right * acceleration);
        }
    }



    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "Player")
        {
            col.gameObject.transform.SetParent(transform);
        }


    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.name == "Player")
        {
            col.gameObject.transform.SetParent(null);
        }
    }
}


