using UnityEngine;

public class HitBox : MonoBehaviour
{
    private  EnemyHealth health;

    private void Awake()
    {
        health = GetComponentInParent<EnemyHealth>();
    }

    public void OnRaycastHit(Shot shot)
    {
        health.TakeDamage(shot.damage);
    }
}
