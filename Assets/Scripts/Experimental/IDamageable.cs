using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public interface IDamageable
{
    public float MaxHealth { get; set; }
    public float CurrentHealth { get; set; }
    public void LoseHealth(float amount);
    public void HealHealth(float amount);
    public void Die();
}
