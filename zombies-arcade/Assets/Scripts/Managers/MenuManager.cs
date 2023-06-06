using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    private void Start()
    {
        // Hide cursor on lock
        Cursor.visible = true;
    }

    public void StartGame()
    {
        SceneManager.LoadSceneAsync("GameScene");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
