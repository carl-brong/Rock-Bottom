using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableObject : MonoBehaviour
{
    [SerializeField] GameObject Consumable;
    // Start is called before the first frame update
    void Start()
    {
        Consumable = GetComponent<GameObject>();
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(Consumable);
            Destroy(gameObject);
        }
    }
}
