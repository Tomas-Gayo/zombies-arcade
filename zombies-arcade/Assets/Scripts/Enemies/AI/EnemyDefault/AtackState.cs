using UnityEngine;

public class AtackState : IEnemyState
{
    EnemyAI myEnemy;

    // Save reference of the enemy AI
    public AtackState(EnemyAI enemy)
    {
        myEnemy = enemy;
    }

    public void UpdateState() { myEnemy.navMeshAgent.isStopped = true; }
    public void GoToIdleState() { }
    public void GoToChaseState()
    {
        myEnemy.currentState = myEnemy.chaseState;
    }
    public void GoToAttackState() { }
    public void OnTriggerEnter(Collider other) { }
    public void OnTriggerStay(Collider other)
    {
        if (myEnemy.health.isDead)
            return;

        if (other.gameObject.CompareTag("Player"))
        {
            // Trigger animation
            myEnemy.anim.SetBool("isAttacking", true);

            // Am I still on range?
            float distance = Vector3.Distance(myEnemy.transform.position, other.transform.position);
            if (distance > myEnemy.attackDistance)
            {
                myEnemy.anim.SetBool("isAttacking", false);
                GoToChaseState();
            }
        }
    }

    public void OnTriggerExit(Collider other) { }
}