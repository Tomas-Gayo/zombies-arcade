using UnityEngine;

public class ChaseState : IEnemyState
{
    EnemyAI myEnemy;

    Transform target;

    // Save reference of the enemy AI
    public ChaseState(EnemyAI enemy)
    {
        myEnemy = enemy;
    }

    public void UpdateState()
    {
        if (target != null && !myEnemy.health.isDead)
        {
            myEnemy.navMeshAgent.isStopped = false;
            myEnemy.navMeshAgent.destination = target.position;
            myEnemy.anim.SetBool("isWalking", true);
        }

    }
    public void GoToIdleState() 
    {
        myEnemy.currentState = myEnemy.idleState;
    }
    public void GoToChaseState() { }
    public void GoToAttackState()
    {
        myEnemy.currentState = myEnemy.atackState;
    }
    public void OnTriggerEnter(Collider other) { }

    public void OnTriggerStay(Collider other)
    {
        OnPlayerDetected(other);
    }

    public void OnTriggerExit(Collider other) 
    { 
        target = null;
        myEnemy.navMeshAgent.isStopped = true;
        myEnemy.anim.SetBool("isWalking", false);
        GoToIdleState();

    }

    private void OnPlayerDetected(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            target = other.gameObject.transform;
            float distance = Vector3.Distance(myEnemy.transform.position, other.transform.position);

            if (distance <= myEnemy.attackDistance)
            {
                GoToAttackState();
            }
        }
    }

}
