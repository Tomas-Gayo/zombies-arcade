using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealth : MonoBehaviour
{   [Header("Health parameters")]
    [Tooltip("Total Health of the enemy.")]
    public float totalLife = 100;
    [Header("HUD elements")]
    [Tooltip("UI slider for health bar.")]
    public Image healthBar;
    [Tooltip("Health value displayed on the UI.")]
    public TextMeshProUGUI healthText;

    public ParticleSystem hitEffect;

    [HideInInspector]
    public bool isDead;

    private float currentHealth;                  // Current health of the player
    // References
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        currentHealth = totalLife;
        healthText.text = currentHealth.ToString() + " %";
        healthBar.fillAmount = currentHealth * 0.01f;
    }

    private void Update()
    {
        UpdatePlayerStatusHUD();
    }

    public void Damage(float damage)
    {
        if (isDead)
            return;

        currentHealth -= damage;
        hitEffect.Emit(1);
        if (currentHealth <= 0)
        {
            isDead = true;
            currentHealth = 0;
            animator.SetTrigger("Dead");
            //GameManager.instance.UpdateGameState(GameState.Lose);
        }
        animator.SetTrigger("Hit");
    }

    public void Heal(float life)
    {
        currentHealth += life;

        if (currentHealth > totalLife)
        {
            currentHealth = totalLife;
        }
    }

    // Updates health status on the HUD
    public void UpdatePlayerStatusHUD()
    {
        // The UI of the health will be set by modifying the slider amount
        healthBar.fillAmount = currentHealth * 0.01f;
        healthText.text = currentHealth.ToString() + " %";
    }
}
