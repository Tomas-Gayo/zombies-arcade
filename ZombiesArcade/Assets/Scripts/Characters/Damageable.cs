using UnityEngine;
using UnityEngine.Events;
using ScriptableObjectArchitecture;

public class Damageable : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private HealthConfigSO healthConfigSO;
    [SerializeField] private HealthSO healthSO;

    [Header("Broadcasting On")]
    [SerializeField] private HealthSORequestGameEvent updateHealthUI;

    [Header("Events")]
    [SerializeField] private UnityEvent onDead;

    private void Awake()
    {
        if (healthSO == null)
        {
            healthSO = ScriptableObject.CreateInstance<HealthSO>();
            healthSO.MaxShield = healthConfigSO.InitialShield;
            healthSO.MaxHealth = healthConfigSO.InitialHealth;
        }
        healthSO.CurrentShield = healthSO.MaxShield;
        healthSO.CurrentHealth = healthSO.MaxHealth;

        HealthUITrigger();
    }

    public void ReceiveAnAttack(float damage)
    {
        // Si esta muerto devolver

        healthSO.TakeDamage(damage);

        HealthUITrigger();

        if (healthSO.CurrentHealth <= 0)
        {
            onDead?.Invoke();
        }
    }

    public void RestoreHealth(RestoreHealthSORequest request)
    {
        healthSO.Heal(request.healthAmount, request.shieldAmount);
        HealthUITrigger();
    }

    private void HealthUITrigger()
    {
        var healthRequest = new HealthSORequest(
            healthSO: healthSO
        );
        updateHealthUI?.Raise(healthRequest);
    }
}
