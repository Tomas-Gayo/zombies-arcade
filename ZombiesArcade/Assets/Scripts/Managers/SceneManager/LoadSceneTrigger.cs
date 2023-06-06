using UnityEngine;
using ScriptableObjectArchitecture;

public class LoadSceneTrigger : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] private SceneSO sceneToLoad;
    [SerializeField] private bool loadingScreen;

    [Header("Dependencies")]
    [SerializeField] private PlayerPathSO playerPath;
    [SerializeField] private LevelEntranceSO levelEntrance;


    [Header("Broadcasting on")]
    [SerializeField] private LoadSceneRequestGameEvent loadSceneRequest;

    public void SendLoadSceneRequest()
    {
        if (levelEntrance != null && playerPath != null)
            playerPath.path = levelEntrance;

        var request = new LoadSceneRequest(
            scene: sceneToLoad,
            loadingScreen: loadingScreen
        );

        loadSceneRequest?.Raise(request);
    }
}
