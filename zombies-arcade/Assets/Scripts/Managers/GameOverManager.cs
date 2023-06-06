using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    [Tooltip("Add here all the buttons on screen.")]
    public GameObject[] buttons;
    [Tooltip("GameObject of the score text section.")]
    public GameObject scoreResults;
    [Tooltip("Delay time for the buttons to be displayed on the screen.")]
    public float delayTime;

    public Text titleScore;
    public Text scoreText;

    private void Start()
    {
        // Hide cursor on lock
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;

        StartCoroutine(DisplayUI());

        if (Scores.isHighScore)
        {
            titleScore.text = "New Record!";
            scoreText.text = PlayerPrefs.GetInt("HighScore").ToString();
        }
        else
        {
            titleScore.text = "Total Score";
            scoreText.text = PlayerPrefs.GetInt("Score").ToString();
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadSceneAsync("GameScene");
    }

    public void BackMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    private IEnumerator DisplayUI()
    {
        yield return new WaitForSeconds(delayTime);

        foreach (GameObject button in buttons)
        {
           button.SetActive(true);
        }

        scoreResults.SetActive(true);
    }

}
