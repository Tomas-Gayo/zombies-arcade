using UnityEngine;
using ScriptableObjectArchitecture;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
public class HealthBox : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] private int healthAmount;
    [SerializeField] private int shielAmount;

    [Header("Broadcasting on")]
    [SerializeField] private RestoreHealthSORequestGameEvent restoreHealthEvent;
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

        HealthRestoreRequest();
        isPicked = true;
        PickEffects();
        Destroy(gameObject, 2f);
    }

    private void PickEffects()
    {
        audioSource.Play();
        animator.enabled = true;
    }

    private void HealthRestoreRequest()
    {
        var HealthRequest = new RestoreHealthSORequest(
            healthAmount: healthAmount,
            shieldAmount: shielAmount
        );

        restoreHealthEvent?.Raise(HealthRequest);
    }
}
