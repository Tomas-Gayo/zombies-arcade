using UnityEngine;

public class PatrolState : IEnemyDroneState
{
    EnemyDroneAI myEnemy;
    private int nextWayPoint = 0;


    // Save reference of the enemy AI
    public PatrolState(EnemyDroneAI enemy)
    {
        myEnemy = enemy;
    }

    public void UpdateState()
    {
        myEnemy.myLight.color = myEnemy.color;

        // We can decide if the enemy is going to patrol or just be static
        if (!myEnemy.isStatic)
        {
            // Movement start point
            myEnemy.navMeshAgent.destination =
                myEnemy.wayPoints[nextWayPoint].position;
            // Next movement point
            if (Vector3.Distance(myEnemy.transform.position, myEnemy.wayPoints[nextWayPoint].position) < myEnemy.navMeshAgent.stoppingDistance)
            {
                nextWayPoint = (nextWayPoint + 1) %
                    myEnemy.wayPoints.Length;
            }
        }
    }

    public void Impact()
    {
        GoToAttackState();
    }

    public void GoToPatrolState() { }

    public void GoToAlertState()
    {
        myEnemy.navMeshAgent.isStopped = true;
        myEnemy.currentState = myEnemy.alertState;
    }

    public void GoToChaseState() { }

    public void GoToAttackState()
    {
        myEnemy.navMeshAgent.isStopped = true;
        myEnemy.currentState = myEnemy.attackState;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            GoToAlertState();
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            GoToAlertState();
    }

    public void OnTriggerExit(Collider other) { }
}
