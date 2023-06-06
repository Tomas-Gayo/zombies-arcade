using UnityEngine;

public class Shot : MonoBehaviour
{
    [Tooltip("Damage for bullet hit.")]
    public float damage;
    [Header("Weapon Effects")]
    [Tooltip("Particles for muzzle flash effect.")]
    public ParticleSystem[] muzzleFlash;            // In an array 'cause a bug in unity that makes the children particles fly around
    [Tooltip("Hit effect of the bullet.")]
    public ParticleSystem defaultEffect;
    [Tooltip("Hit effect of the bullet on the enemy.")]
    public ParticleSystem enemyHitEffect;
    [Tooltip("Bullet trail effect.")]
    public TrailRenderer trail;
    [Header("Origin and Destination points")]
    public Transform firePoint;                     // firePoint in the weapon
    public Transform fireTarget;                    // AimTarget in the camera

    // References
    private AudioSource audioSource;

    Ray ray;
    RaycastHit hit;

    private void Awake()
    {
        audioSource = GetComponentInChildren<AudioSource>();
    }

    // Called when shooting
    public void Shooting()
    {
        audioSource.Play();
        foreach (var muzzle in muzzleFlash)
        {
            muzzle.Emit(1);
        }

        ray.origin = firePoint.position;
        ray.direction = fireTarget.position - firePoint.position;

        var trace = Instantiate(trail, ray.origin, Quaternion.identity);
        trace.AddPosition(ray.origin);
        
        if(Physics.Raycast(ray, out hit))
        {
            trace.transform.position = hit.point;

            var enemy = hit.collider.GetComponent<HitBox>();
            if (enemy) 
            {
                ShotEffects(enemyHitEffect);
                enemy.OnRaycastHit(this);

            } 
            else
            {
                ShotEffects(defaultEffect);
            }


        }
    }

    private void ShotEffects(ParticleSystem effect)
    {
        effect.transform.position = hit.point;
        effect.transform.forward = hit.normal;
        effect.Emit(1);
    }
}
