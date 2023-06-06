using UnityEngine;
using ScriptableObjectArchitecture;

[RequireComponent(typeof(Animator))]
public class LoadingScreenUI : MonoBehaviour
{
    [Header("Broadcasting on")]
    [SerializeField] BoolGameEvent toggleLoadingScreen;
    // Private dependencies
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void ToggleScreen(bool toggle)
    {
        if (toggle)
        {
            animator.SetTrigger("Show");
        }
        else
        {
            animator.SetTrigger("Hide");
        }
    }

    public void ShowLoadingScreenEvent()
    {
        toggleLoadingScreen?.Raise(true);
    }

    public void HideLoadingScreenEvent()
    {
        toggleLoadingScreen?.Raise(false);
    }
}
