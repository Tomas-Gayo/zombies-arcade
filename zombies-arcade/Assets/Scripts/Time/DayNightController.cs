using UnityEngine;

public class DayNightController : MonoBehaviour
{
    [Tooltip("The gameobject that represent the light of the sun.")]
    public GameObject directionalLight;
    [Tooltip("The time percentage of the day the street lights will turn on.")]
    [Range(0f, 1f)]
    public float startDaylight;    
    [Tooltip("The time percentage of the day the street lights will turn off.")]
    [Range(0f, 1f)]
    public float endDayLight;
    [Tooltip("Check it if you want to start the day at daylight otherwise will be night.")]
    public bool isDaylight;

    // References
    private TimeManager time;                   
    //private GameObject[] nightLights;           // Reference to the objects in scene that have night lights


    private void Awake()
    {
        // Get all the lights in the scene
        //nightLights = GameObject.FindGameObjectsWithTag("NightLight");
        time = GetComponent<TimeManager>();
    }

    private void Update()
    {
        // Update the day rotation
        UpdateDirectionalLight();
        UpdateDayNight();
        // Update all the street lights
        //UpdateLights();
    }

    private void UpdateDirectionalLight()
    {
        if (directionalLight != null)
            directionalLight.transform.localRotation = Quaternion.Euler(new Vector3((time.timePercentage * 360f) - 90f, 170f, 0));
    }
    private void UpdateDayNight()
    {
        // Depending on the percentage of the day we know if is daylight or not
        if (time.timePercentage > startDaylight && time.timePercentage < endDayLight)
        {
            isDaylight = true;
        }
        else
        {
            isDaylight = false;
        }

    }

    /**
    private void UpdateLights()
    {
        for (int i = 0; i < nightLights.Length; i++)
        {
            // Choose a time of the day to switch the lights ON or OFF
            if (isDaylight)
            {
                nightLights[i].GetComponentInChildren<Light>().enabled = false;
            }
            else
            {
                nightLights[i].GetComponentInChildren<Light>().enabled = true;
            }
        }
    }
    */
}
