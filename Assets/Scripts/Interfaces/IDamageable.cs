public interface IDamageable
{
    public float MaxHealth { get; set; }
    public float CurrentHealth { get; set; }
    public void LoseHealth(float amount);
    public void HealHealth(float amount);
    public void Die();
}
