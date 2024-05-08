using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBoost : MonoBehaviour
{
    [SerializeField] private float boost;
    public GameObject healthBoost;
    private void OnTriggerEnter2D(Collider2D collision)
    
    {
        Health playerHealth = collision.GetComponent<Health>();
        if (playerHealth != null && collision.gameObject.CompareTag("Player"))
        {
            playerHealth.takeDamage(-boost);
            Destroy(healthBoost);
        }
    }
        //if (collision.gameObject.CompareTag("Player")){
            
    //}
}
