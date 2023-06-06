using UnityEngine;

[CreateAssetMenu(fileName = "NewHealth", menuName = "Scriptable Objects/Health", order = 2)]
public class HealthSO : ScriptableObject
{
    [Header("Configuration")]
    [SerializeField] private float maxShield;
    [SerializeField] private float currentShield;
    [SerializeField] private float maxHealth;
    [SerializeField] private float currentHealth;
    [Tooltip("Health damage when there is still a shield")]
    [SerializeField, Range(1, 10)] private int shieldProtection;

    public float MaxShield { get { return maxShield; } set { maxShield = value; } }
    public float MaxHealth { get { return maxHealth; } set { maxHealth = value; } }

    public float CurrentShield
    {
        get { return currentShield; }
        set 
        {
            currentShield = value; 
            if (currentShield > maxShield)
                currentShield = maxShield;
            else if (currentShield <= 0)
                currentShield = 0;
        }
    }

    public float CurrentHealth
    {
        get { return currentHealth; }
        set 
        { 
            currentHealth = value; 
            if (currentHealth > maxHealth)
                currentHealth = maxHealth;
            else if (currentHealth <= 0)
                currentHealth = 0;
        }
    }

    public void TakeDamage(float damage)
    {
        if (currentShield > 0)
        {
            CurrentShield -= damage;
            CurrentHealth -= damage * 1 / shieldProtection;
        }
        else
        {
            CurrentHealth -= damage;
        }
    }

    public void Heal(float healAmount, float shieldAmount)
    {
        CurrentShield += shieldAmount;
        CurrentHealth += healAmount;
    }
}
