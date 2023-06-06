using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{

    [Tooltip("UI element to display the time.")]
    public Text watch;

    [Header("Time Parameters")]
    [Tooltip("How much time (in seconds) will last one day.")]
    public float totalTime;
    [Range(0f, 1f)]
    [Tooltip("Percentage of the time that has passed, so from 0 to 1 means 00:00 h to 23:59 h")]
    public float timePercentage;
    
    private float currentTime;                  // Current time in game

    private void Start()
    {
        currentTime = totalTime * timePercentage;
    }

    private void Update()
    {
        // Increase time when game is running
        if (Application.isPlaying)
            currentTime += Time.deltaTime;

        // Clamp the time to its maximum so the next day can start
        if (currentTime > totalTime)
            currentTime = 0;

        // Update the UI to show the current time
        UpdateWatchUI();

        // Day percentage  
        timePercentage = currentTime / totalTime;
    }
     
    private void UpdateWatchUI()
    {
        if (watch != null)
            watch.text = ConvertTimeToString((currentTime * 24) / totalTime);
    }

    // This functions returns the time in the international format of 24 hours giving a time variable
    private string ConvertTimeToString(float time)
    {
        string minutes = ((int)(time * 60) % 60).ToString("00");
        string hours = ((int)time % 24).ToString("00");

        return hours + ":" + minutes;
    }
}
