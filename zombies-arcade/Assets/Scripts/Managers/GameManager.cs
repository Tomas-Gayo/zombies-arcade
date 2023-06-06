using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Time management")]
    [Tooltip("Delay before start a game.")]
    public float startDelay = 5f;
    [Tooltip("Delay after player dies.")]
    public float endTime = 2f;
    [Tooltip("Pass the time manager game object")]
    public GameObject timeManager;

    [Header("Spawns")]
    [Tooltip("Spawn transform for the drops")]
    public Transform[] dropSpawns;
    public GameObject[] drops;
    [Tooltip("Spawn transform for the enemies of the day.")]
    public Transform[] enemySpawnDay;
    [Tooltip("Spawn transform for the enemies of the night.")]
    public Transform[] enemySpawnNight;
    [Tooltip("Prefabs of the enemies you want to spawn randomly at the day.")]
    public GameObject[] enemyDay;
    [Tooltip("Prefabs of the enemies you want to spawn randomly at the night.")]
    public GameObject[] enemyNight;

    [Header("UI elements")]
    [Tooltip("GameObject of the section of the day.")]
    public GameObject dayTitleObject;
    [Tooltip("Text of the day.")]
    public TextMeshProUGUI dayText;
    [Tooltip("Objectives panel.")]
    public GameObject objectivesPanel;
    [Tooltip("Objective title UI text")]
    public TextMeshProUGUI objectiveTitleText;
    [Tooltip("Objectives UI text")]
    public TextMeshProUGUI objectiveOneText;
    [Tooltip("Objectives UI text")]
    public TextMeshProUGUI objectiveTwoText;

    [Tooltip("Pass the time manager game object")]
    public AudioManager audioManager;

    private int days;                         // The total of rounds played by the user
    private GameObject playerInstance;        // Instance of the player

    // References
    private ScoreController score;
    private DayNightController time;

    private void Awake()
    {
        score = GetComponent<ScoreController>();
        time = timeManager.GetComponent<DayNightController>();

        // Save Player reference only on the start
        playerInstance = GameObject.FindGameObjectWithTag("Player");
    }

    private void Start()
    {
        // Initialize days
        days = 0;

        // Hide cursor on lock
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        // Clean texts
        dayText.text = string.Empty;
        objectivesPanel.SetActive(false);
        objectiveTitleText.text = string.Empty;
        objectiveOneText.text = string.Empty;
        objectiveTwoText.text = string.Empty;

        // Start Loop
        StartCoroutine(GameLoop());
    }

    private void Update()
    {
        if (playerInstance != null)
            CheckPlayerStatus();
    }

    private IEnumerator GameLoop()
    {
        yield return StartCoroutine(StartingRound());

        yield return StartCoroutine(DayPhase());

        yield return StartCoroutine(NightPhase());

        StartCoroutine(GameLoop());
    }

    private IEnumerator StartingRound()
    {
        // Starting round feedback
        days++;
        dayTitleObject.SetActive(true);
        dayText.text = "Day " + days;

        // Player cannot move on this part
        //DisablePlayer();

        // Switch or start music
        audioManager.PlayMusic();

        // Reset enemies and drops at the start of the day
        DestroySpawnObjects("Enemy");
        DestroySpawnObjects("Drops");

        // Gives some points when survive a day
        if (days > 2)
            score.Add(Scores.survive);

        yield return new WaitForSeconds(startDelay);
    }

    private IEnumerator DayPhase()
    {
        // Now the game can start so we acivate the time counter and the player movement
        timeManager.SetActive(true);
        //EnablePlayer();

        // UI Feedback for the day phase
        dayTitleObject.SetActive(false);
        objectivesPanel.SetActive(true);
        SetDayObjectives();

        // Spawn drops and enemies
        SpawnObjects(drops, dropSpawns);
        SpawnObjects(enemyDay, enemySpawnDay);


        // While is daytime do not change this phase
        while (time.isDaylight)
            yield return null;
    }

    private IEnumerator NightPhase()
    {
        audioManager.PlayMusic();
        SetNightObjectives();

        // More Enemies
        SpawnObjects(enemyNight, enemySpawnNight);

        // While is night do not change this phase
        while (!time.isDaylight)
            yield return null;
    }

    private void SpawnObjects(GameObject[] obj, Transform[] spawn)
    {
        for (int i = 0; i < spawn.Length; i++)
        {
            Instantiate(obj[Random.Range(0, obj.Length)], spawn[i].transform.position, Quaternion.identity);
        }
    }

    private void DestroySpawnObjects(string objects)
    {
        GameObject[] gob = GameObject.FindGameObjectsWithTag(objects);

        if (gob == null) { return; }

        foreach (GameObject go in gob)
        {
            Destroy(go);
        }
    }

    private void SetDayObjectives()
    {
        objectiveTitleText.text = "DAYLIGHT: Find Resources";
        objectiveOneText.text = "Kill zombies <b> 10 points </b>";
        objectiveTwoText.text = "Purple objects <b>100 points</b>";
    }

    private void SetNightObjectives()
    {
        objectiveTitleText.text = "NIGHT: Kill & Survive";
        objectiveOneText.text = "Kill zombies <b> 50 points </b>";
        objectiveTwoText.text = "Survive! <b>500 points</b>";
    }

    private void CheckPlayerStatus()
    {
        PlayerHealth player = playerInstance.gameObject.GetComponent<PlayerHealth>();

        if (player.isDead)
        {
            StartCoroutine(OnPlayerDead());
        }
    }

    private IEnumerator OnPlayerDead()
    {
        //DisablePlayer();
        score.CheckResults();
        yield return new WaitForSeconds(endTime);
        SceneManager.LoadSceneAsync("GameOver");
    }

    private void DisablePlayer()
    {
        playerInstance.gameObject.GetComponent<PlayerMovement>().enabled = false;
    }

    private void EnablePlayer()
    {
        playerInstance.gameObject.GetComponent<PlayerMovement>().enabled = true;
    }
}
