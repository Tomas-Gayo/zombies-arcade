using UnityEngine;

public class IdleState : IEnemySpiderState
{
    EnemySpiderAI myEnemy;

    // Save reference of the enemy AI
    public IdleState(EnemySpiderAI enemy)
    {
        myEnemy = enemy;
    }

    public void UpdateState()
    {
        myEnemy.myLight.color = myEnemy.color;
    }

    public void GoToChaseState() 
    {
        myEnemy.anim.SetTrigger("Walk");
        myEnemy.currentState = myEnemy.chaseSpiderState;
    }
    public void GoToExplosionState() { }
    public void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag("Player"))
            GoToChaseState();
    }

    public void OnTriggerStay(Collider other) 
    {
        if (other.gameObject.CompareTag("Player"))
            GoToChaseState();
    }
    public void OnTriggerExit(Collider other) { }
}

