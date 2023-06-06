using UnityEngine;

public class CollectableDrop : MonoBehaviour
{
    private ScoreController score;
    private AudioSource audioSource;
    private bool isPicked = false;          // If is picked up cannot be pick it up again

    private void Awake()
    {
        score = FindObjectOfType<ScoreController>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isPicked)
            return;

        if (other.gameObject.CompareTag("Player"))
        {
            isPicked = true;
            score.Add(Scores.collect);
            PickEffects();
            Destroy(gameObject, 1f);
        }
    }

    private void PickEffects()
    {
        audioSource.Play();
    }
}
