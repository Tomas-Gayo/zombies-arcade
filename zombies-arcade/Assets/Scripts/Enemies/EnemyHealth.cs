using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float totalHealth;
    [HideInInspector]
    public float currentHealth;
    [Tooltip("Particle effect on enemy dead.")]
    public ParticleSystem deadEffect;

    [HideInInspector]
    public bool isDead;

    // References
    private Animator animator;
    private ScoreController score;
    private DayNightController time;
    private AudioSource audioSource;
    private EnemyDrop drop;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        score = FindObjectOfType<ScoreController>();
        audioSource = GetComponent<AudioSource>();
        drop = GetComponent<EnemyDrop>();
    }

    private void Start()
    {
        currentHealth = totalHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        animator.SetTrigger("Hit");
        audioSource.Play();

        if (currentHealth <= 0)
        {
            isDead = true;
            animator.SetTrigger("Dead");
            deadEffect.Emit(1);

            drop.DropObject();

            Invoke(nameof(OnEnemyDead), 2f);

            AddScore();
        }
    }

    private void AddScore()
    {
        time = FindObjectOfType<DayNightController>();

        // Add score
        if (time.isDaylight)
            score.Add(Scores.killEnemyDaylight);
        else
            score.Add(Scores.killEnemyNight);
    }

    private void OnEnemyDead()
    {
        isDead = false;
        currentHealth = totalHealth;
        gameObject.SetActive(false);
    }
}
