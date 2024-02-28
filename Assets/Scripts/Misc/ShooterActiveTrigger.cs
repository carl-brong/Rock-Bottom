using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterActiveTrigger : MonoBehaviour
{

    private GameObject _shooter;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

    private void Awake()
    {
        _shooter = GetComponentInParent<GameObject>();
    }
}
