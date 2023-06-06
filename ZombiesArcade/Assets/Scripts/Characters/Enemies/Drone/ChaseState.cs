using UnityEngine;

public class ChaseState : IEnemyDroneState
{
    EnemyDroneAI myEnemy;

    Transform target;

    public ChaseState(EnemyDroneAI enemy)
    {
        myEnemy = enemy;
    }

    public void UpdateState()
    {
        myEnemy.myLight.color = Color.blue;
        if (target != null)
        {
            myEnemy.transform.position = Vector3.MoveTowards(myEnemy.transform.position, target.position, myEnemy.speed * Time.deltaTime);
            myEnemy.transform.LookAt(target.position);
        }
    }

    public void Impact() { }

    public void GoToPatrolState() { }
    public void GoToAlertState()
    {
        myEnemy.currentState = myEnemy.alertState;
    }
    public void GoToChaseState() { }
    public void GoToAttackState() 
    {
        myEnemy.currentState = myEnemy.attackState;
    }


    public void OnTriggerEnter(Collider other) { }

    public void OnTriggerStay(Collider other) 
    { 
        if (other.gameObject.CompareTag("Player"))
        {
            target = other.gameObject.transform;

            float distance = Vector3.Distance(myEnemy.transform.position, other.transform.position);
            if (distance <= myEnemy.shotRange)
            {
                GoToAttackState();
            }
            
            if (distance >= myEnemy.alertRange)
            {
                GoToAlertState();
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            GoToAlertState();
    }
}
