using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingSceneManager : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] private LoadingScreenUI loadingScreenUI;

    // Private references
    private LoadSceneRequest pendingRequest;

    public void OnLoadMenuRequest(LoadSceneRequest request)
    {
        if (!IsSceneAlreadyLoaded(request.scene))
            SceneManager.LoadScene(request.scene.sceneName);
    }

    public void OnLoadLevelRequest(LoadSceneRequest request)
    {
        if (IsSceneAlreadyLoaded(request.scene))
        {
            ActivateScene(request);
        }
        else
        {
            if (request.loadingScreen)
            {
                pendingRequest = request;
                loadingScreenUI.ToggleScreen(true);
            }
            else
            {
                StartCoroutine(ProcessSceneLoading(pendingRequest));
            }
        }
    }
    public void OnLoadingScreenToggled(bool enabled)
    {
        if (enabled && pendingRequest != null)
            StartCoroutine(ProcessSceneLoading(pendingRequest));
    }

    private bool IsSceneAlreadyLoaded(SceneSO scene)
    {
        Scene sceneToLoad = SceneManager.GetSceneByName(scene.sceneName);

        if (sceneToLoad != null && sceneToLoad.isLoaded)
            return true;
        else
            return false;
    }

    private IEnumerator ProcessSceneLoading(LoadSceneRequest request)
    {
        if (request.scene != null)
        {
            Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.UnloadSceneAsync(currentScene);

            AsyncOperation loadSceneProcess = SceneManager.LoadSceneAsync(request.scene.sceneName, LoadSceneMode.Additive);

            while (!loadSceneProcess.isDone)
                yield return null;

            ActivateScene(request);
        }
    }

    private void ActivateScene(LoadSceneRequest request)
    {
        Scene sceneToActivate = SceneManager.GetSceneByName(request.scene.name);
        SceneManager.SetActiveScene(sceneToActivate);

        if (request.loadingScreen)
            loadingScreenUI.ToggleScreen(false);

        pendingRequest = null;
    }
}
