using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempFinish : MonoBehaviour
{
    [SerializeField] private GameObject _gm;

    private ReturnToTitle _toTitle;
    
    private void Awake()
    {
        _toTitle = _gm.GetComponent<ReturnToTitle>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _toTitle.ExitToTitle();
        }
    }
}
