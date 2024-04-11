using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelProgressUpdater : MonoBehaviour
{
    public int level;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerPrefs.SetInt("Progress", level);
            Debug.Log(PlayerPrefs.GetInt("Progress"));
        }
    }
}
