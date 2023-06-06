using UnityEngine;

public class AmmoDrop : MonoBehaviour
{
    [Tooltip("Amount of ammo will be restored.")]
    public int ammo;

    private PlayerShooting player;            // Reference to the script that cures the player
    private AudioSource audioSource;
    private bool isPicked = false;          // If is picked up cannot be pick it up again

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isPicked)
            return;

        if (other.gameObject.CompareTag("Player"))
        {
            player = other.gameObject.GetComponentInChildren<PlayerShooting>();
            if (player != null)
            {
                isPicked = true;
                player.RestoreAmmo(ammo);
                PickEffects();
                Destroy(gameObject, 1f);
            }
        }
    }

    private void PickEffects()
    {
        audioSource.Play();
    }
}

