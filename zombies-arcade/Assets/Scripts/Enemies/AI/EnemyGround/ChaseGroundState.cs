using UnityEngine;

public class ChaseGroundState : IEnemyGroundState
{
    EnemyGroundAI myEnemy;

    Transform target;

    // Save reference of the enemy AI
    public ChaseGroundState(EnemyGroundAI enemy)
    {
        myEnemy = enemy;
    }

    public void UpdateState()
    {

        if (target != null && !myEnemy.health.isDead)
        { 
            myEnemy.navMeshAgent.isStopped = false;
            myEnemy.navMeshAgent.destination = target.position;
        }

        AnimatorStateInfo animatorInfo = myEnemy.anim.GetCurrentAnimatorStateInfo(0);

        if (animatorInfo.IsTag("StandUp"))
        {
            myEnemy.navMeshAgent.isStopped = true;
        }
        else
        {
            myEnemy.navMeshAgent.isStopped = false;
        }
    }

    public void GoToChaseState() { }
    public void GoToAttackState()
    {
        myEnemy.currentState = myEnemy.atackGroundState;
    }
    public void OnTriggerEnter(Collider other) { }

    public void OnTriggerStay(Collider other)
    {
        OnPlayerDetected(other);
    }

    public void OnTriggerExit(Collider other) { }

    private void OnPlayerDetected(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            target = other.gameObject.transform;
            float distance = Vector3.Distance(myEnemy.transform.position, other.transform.position);

            if (distance < myEnemy.standUpDistance)
            {
                myEnemy.anim.SetTrigger("StandUp");
                myEnemy.navMeshAgent.isStopped = true;
            }

            if (distance <= myEnemy.attackDistance)
                GoToAttackState();
            
        }
    }

}
