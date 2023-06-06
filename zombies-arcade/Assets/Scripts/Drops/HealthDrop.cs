using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDrop : MonoBehaviour
{
    [Tooltip("The amount of heal the player's health is going to recover.")]
    public int heal = 25;

    private PlayerHealth player;            // Reference to the script that cures the player
    private AudioSource audioSource;
    private Animator animator;
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
            player = other.gameObject.GetComponent<PlayerHealth>();
            if (player != null)
            {
                isPicked = true;
                player.Heal(heal);
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

