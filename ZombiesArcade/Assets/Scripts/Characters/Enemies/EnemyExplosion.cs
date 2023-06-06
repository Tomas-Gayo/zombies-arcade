using UnityEngine;
using UnityEngine.Events;

public class EnemyExplosion : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] private int damage;
    [SerializeField] private float explosionRadius;
    [SerializeField] private float explosionForce;

    [Header("Effects")]
    [SerializeField] private ParticleSystem explosionParticles;

    [Header("Events")]
    [SerializeField] private UnityEvent onDead;

    private bool hasExplode = false;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Explode()
    {
        if (hasExplode)
            return;

        // Play some nice effects
        ExplosionEffect();

        hasExplode = true;
        // Creates a physic sphere where the player can be damaged
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, explosionRadius);
        foreach (Collider hit in colliders)
        {
            if (hit.CompareTag("Player")) {

                // Add a force to all the physic objects that collides with this explosion
                Rigidbody rb = hit.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.AddExplosionForce(explosionForce, explosionPos, explosionRadius);
                }

                // Substract life to the player in case it hits
                if (hit.TryGetComponent(out Damageable damageable))
                    damageable.ReceiveAnAttack(damage);
            }
        }
      
        GetComponentInChildren<BoxCollider>().enabled = false;
        Destroy(gameObject, explosionParticles.main.duration);
        onDead?.Invoke();

    }

    private void ExplosionEffect()
    {   
        Instantiate(explosionParticles, transform.position, transform.rotation);
        animator.SetTrigger("Die");
    }
}
