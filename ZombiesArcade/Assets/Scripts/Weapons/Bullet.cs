using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Configuration")]
    [Tooltip("Damage of the bullet.")]
    [SerializeField] public int damage;
    [Tooltip("Force of the bullet when moving.")]
    [SerializeField] private float bulletForce;
    [Tooltip("Time the bullet is on scene.")]
    [SerializeField] private float bulletLifeTime;
    [Tooltip("Time the explosion is on scene.")]
    [SerializeField] private float explosionLifeTime;

    [Header("Effects")]
    [SerializeField] private GameObject explosionParticles;

    private void Start()
    {
        ShotBullet();
    }

    private void ShotBullet()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * bulletForce, ForceMode.Impulse);
        Destroy(gameObject, bulletLifeTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            if (collision.collider.TryGetComponent(out Damageable damageable))
                damageable.ReceiveAnAttack(damage);
        }
        GameObject explosion = Instantiate(explosionParticles, transform.position, transform.rotation);
        Destroy(explosion, explosionLifeTime);
        Destroy(gameObject);

    }
}
