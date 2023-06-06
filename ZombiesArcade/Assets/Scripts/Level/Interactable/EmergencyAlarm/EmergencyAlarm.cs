using UnityEngine;

public class EmergencyAlarm : MonoBehaviour
{
    [Tooltip("Particles displayed when alarm is destroyed.")]
    public ParticleSystem explosionParticles;

    [Header("Dependencies")]
    [SerializeField] private AudioSource audiosource;

    // When the alarm is hit is going to be destroyed
    public void Destroy()
    {
        audiosource.Play();
        var explosion = Instantiate(explosionParticles, transform.position, transform.rotation);
        Destroy(explosion.gameObject, explosion.main.duration);
        Destroy(gameObject);
    }
}
