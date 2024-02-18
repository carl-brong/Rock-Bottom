using System;
using UnityEngine;

public class PlayerHealthBar : MonoBehaviour
{
    private void Awake()
    {
        Player.PlayerHealthChangeEvent += HandleHealthChange;
    }

    private void HandleHealthChange(float currentHealth) 
    {
        Debug.Log(currentHealth);    
    }
}
