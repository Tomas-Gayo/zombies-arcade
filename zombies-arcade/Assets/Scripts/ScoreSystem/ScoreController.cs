using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    public Text scoreUI;

    private int highScore;
    private int newScore;

    private void Start()
    {
        newScore = 0;
        scoreUI.text = newScore.ToString("000000");
        highScore = PlayerPrefs.GetInt("HighScore");
        Scores.isHighScore = false;
    }

    // For each call we sum a new score
    public void Add(int score)
    {
        newScore += score;
        scoreUI.text = newScore.ToString("000000");
    }

    // Compare de new result with the xistant high Score
    public void CheckResults()
    {   
        // If it is greater then save it
        if (newScore > highScore)
        {
            PlayerPrefs.SetInt("HighScore", newScore);
            Scores.isHighScore = true; 
        }
        else
        {
            PlayerPrefs.SetInt("Score", newScore);
        }
    }
}
