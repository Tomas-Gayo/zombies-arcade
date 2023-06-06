using UnityEngine;

public class ChaseSpiderState : IEnemySpiderState
{
    EnemySpiderAI myEnemy;

    Transform target;

    // Save reference of the enemy AI
    public ChaseSpiderState(EnemySpiderAI enemy)
    {
        myEnemy = enemy;
    }

    public void UpdateState()
    {
        myEnemy.myLight.color = Color.yellow;
        if (target != null)
        {
            myEnemy.transform.position = Vector3.Lerp(myEnemy.transform.position, target.position, myEnemy.speed * Time.deltaTime);
            myEnemy.transform.LookAt(target.position);
        }
            
    }

    public void GoToChaseState() { }
    public void GoToExplosionState()
    {
        myEnemy.currentState = myEnemy.explosionState;
    }
    public void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag("Player"))
        {
            target = other.gameObject.transform;

            float distance = Vector3.Distance(myEnemy.transform.position, other.transform.position);
            if (distance <= myEnemy.explosionRange)
            {
                GoToExplosionState();
            }
        }
    }
    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            target = other.gameObject.transform;

            float distance = Vector3.Distance(myEnemy.transform.position, other.transform.position);
            if (distance <= myEnemy.explosionRange)
            {
                GoToExplosionState();
            }
        }
    }
    public void OnTriggerExit(Collider other) { }
}