using System;
using UnityEngine;

[CreateAssetMenu(menuName = "HealthTracker")]
public class PlayerHealthBar : ScriptableObject
{
    private void OnEnable()
    {
        Player.PlayerHealthChangeEvent += HandleHealthChange;
    }

    private void HandleHealthChange(float currentHealth) 
    {
        Debug.Log(currentHealth);    
    }
}
