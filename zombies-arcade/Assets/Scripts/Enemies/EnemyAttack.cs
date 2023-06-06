using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [Tooltip("Dame of the enemy.")]
    public int damage = 20;
    [Tooltip("Time between attacks.")]
    public float timeBetweenAttacks = 2;

    private bool canAttack;

    private void Start()
    {
        canAttack = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (canAttack)
        {
            canAttack = false;
            // Substract life
            PlayerHealth health = other.GetComponentInParent<PlayerHealth>();
            if (health != null)
            {
                health.Damage(damage);
            }
            Invoke(nameof(Reset), timeBetweenAttacks);
        }
    }
    private void Reset()
    {
        canAttack = true;
    }
}
