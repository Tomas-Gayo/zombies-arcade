using System.Collections;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    [Tooltip("Reference to the audio source of the gun for the fire sound.")]
    [SerializeField] private AudioSource fireSound;
    [Tooltip("Point where the enemy is going to shoot.")]
    [SerializeField] private Transform firePosition;
    [Tooltip("Bullet prefab that will be instantiated")]
    [SerializeField] private GameObject bullet;
    [Tooltip("Time lasted between a sequence of shots.")]
    [SerializeField] private float timeBetweenShooting;
    [Tooltip("Time between shots in a burst.")]
    [SerializeField] private float timeBetweenShots;
    [Tooltip("Quantity of bullets in a single shot.")]
    [SerializeField] private int bulletsShot;

    private int totalBulletsPerShot;     // The total of bullets shot at the same shoot 

    private void Start()
    {
        totalBulletsPerShot = bulletsShot;
    }

    public void Shoot()
    {
        ShotEffect();
        bulletsShot--;

        // We instantiate the bullet here but the damage logic is on the bullet
        Instantiate(bullet, firePosition.position, transform.rotation * Quaternion.Euler(0f, 0f, 0f));

        // Repeat the shot until all the bullets are zero so the burst effect is created
        if (bulletsShot > 0)  Invoke(nameof(Shoot), timeBetweenShots);
        else bulletsShot = totalBulletsPerShot;
    }

    private void ShotEffect()
    {
        fireSound.Play();
    }
}
