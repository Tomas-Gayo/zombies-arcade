using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneInitializer : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private SceneSO[] scenesToLoad;

    [Header("Broadcasting On")]
    [SerializeField] private UnityEvent onDependenciesLoaded;

    private void Start()
    {
        StartCoroutine(LoadDependencies());
    }

    private IEnumerator LoadDependencies()
    {
        for (int i = 0; i < scenesToLoad.Length; i++)
        {
            SceneSO scene = scenesToLoad[i];

            if (!SceneManager.GetSceneByName(scene.name).isLoaded)
            {
                var loadOperation = SceneManager.LoadSceneAsync(scene.sceneName, LoadSceneMode.Additive);

                while (loadOperation.isDone)
                    yield return null;
            }
        }

        onDependenciesLoaded?.Invoke();
    }
}
