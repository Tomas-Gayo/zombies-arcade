using UnityEngine;
using ScriptableObjectArchitecture;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
public class AmmoBox : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] private int ammoAmount;

    [Header("Broadcasting on")]
    [SerializeField] private IntGameEvent restoreAmmoEvent;
    // Private dependencies
    private Animator animator;
    private AudioSource audioSource;
    // Private variables
    private bool isPicked = false;          // If is picked up cannot be pick it up again

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    public void PickObject()
    {
        if (isPicked) return;

        restoreAmmoEvent?.Raise(ammoAmount);
        isPicked = true;
        PickEffects();
        Destroy(gameObject, 2f);
    }
    private void PickEffects()
    {
        audioSource.Play();
        animator.enabled = true;
    }
}
