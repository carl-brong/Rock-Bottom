using System;
using UnityEngine;
using UnityEngine.UIElements;

// Vincent Lee
// 5/2/24

[CreateAssetMenu(menuName = "HealthTracker")]
public class PlayerHealthBar : ScriptableObject
{
    public float healthPercent;
    
    private void OnEnable()
    {
        Player.PlayerHealthChangeEvent += HandleHealthChange;
    }

    private void HandleHealthChange(float currentHealth)
    {
        
        healthPercent = currentHealth / 20;
    }
}
