using UnityEngine;

public class IdleState : IEnemyState
{
    EnemyAI myEnemy;

    // Save reference of the enemy AI
    public IdleState(EnemyAI enemy)
    {
        myEnemy = enemy;
    }

    public void UpdateState() { }
    public void GoToIdleState() { }
    public void GoToChaseState()
    {
        myEnemy.currentState = myEnemy.chaseState;
    }
    public void GoToAttackState() { }
    public void OnTriggerEnter(Collider other)
    {
        OnPlayerDetected(other);
    }

    public void OnTriggerStay(Collider other)
    {
        OnPlayerDetected(other);
    }

    public void OnTriggerExit(Collider other) { }

    private void OnPlayerDetected(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            GoToChaseState();
    }
}
