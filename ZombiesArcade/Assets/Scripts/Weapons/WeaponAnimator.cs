using UnityEngine;

[RequireComponent(typeof(Animator))]
public class WeaponAnimator : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void OnReadyToShot()
    {
        animator.SetBool("IsReloading", false);
    }

    public void OnReload()
    {
        animator.SetBool("IsReloading", true);
    }
}
