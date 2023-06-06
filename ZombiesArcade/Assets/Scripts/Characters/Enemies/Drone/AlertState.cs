using UnityEngine;

public class AlertState : IEnemyDroneState
{
    EnemyDroneAI myEnemy;
    private float currentRotationTime = 0;

    // Save reference of the enemy AI
    public AlertState(EnemyDroneAI enemy)
    {
        myEnemy = enemy;
    }

    public void UpdateState()
    {
        myEnemy.myLight.color = Color.yellow;

        myEnemy.transform.rotation *= Quaternion.Euler(0f,
            Time.deltaTime * 360 * 1f / myEnemy.rotationTime,
            0f);

        if (currentRotationTime > myEnemy.rotationTime) 
        {
            currentRotationTime = 0;
            GoToPatrolState();
        }
        
        currentRotationTime += Time.deltaTime;
    }

    public void Impact()
    {
        GoToAttackState();
    }
    public void GoToPatrolState()
    {
        myEnemy.navMeshAgent.isStopped = false;
        myEnemy.currentState = myEnemy.patrolState;
    }

    public void GoToAlertState() { }

    public void GoToChaseState() 
    {
        myEnemy.navMeshAgent.isStopped = false;
        myEnemy.currentState = myEnemy.chaseState;
        currentRotationTime = 0;
    }

    public void GoToAttackState()
    {
        //myEnemy.currentState = myEnemy.attackState;
    }


    // Nothing to collid here
    public void OnTriggerEnter(Collider other) { }

    public void OnTriggerStay(Collider other)
    {
        float distance = Vector3.Distance(myEnemy.transform.position, other.transform.position);
        if (distance <= myEnemy.shotRange)
        {
            GoToChaseState();        }
    }

    public void OnTriggerExit(Collider other) { }
}
