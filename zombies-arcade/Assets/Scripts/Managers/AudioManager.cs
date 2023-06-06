using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Tooltip("Pass the time manager game object")]
    public GameObject timeManager;
    
    [Tooltip("Music for the day time.")]
    public AudioClip dayMusic;
    [Tooltip("Music for the night time.")]
    public AudioClip nightMusic;

    private DayNightController time;

    //References
    private AudioSource audioscr;

    private void Awake()
    {
        audioscr = GetComponent<AudioSource>();
        time = timeManager.GetComponent<DayNightController>();
    }

    public void PlayMusic()
    { 
        if (time.isDaylight)
        {
            audioscr.clip = dayMusic;
            audioscr.Play();
        }
        else
        {
            audioscr.clip = nightMusic;
            audioscr.Play();
        }
    }
}
