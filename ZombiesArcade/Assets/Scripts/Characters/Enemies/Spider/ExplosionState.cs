using UnityEngine;

public class ExplosionState : IEnemySpiderState
{
    EnemySpiderAI myEnemy;

    // Save reference of the enemy AI
    public ExplosionState(EnemySpiderAI enemy)
    {
        myEnemy = enemy;
    }

    public void UpdateState()
    {
        myEnemy.myLight.color = Color.red;
    }

    public void GoToChaseState() { }
    public void GoToExplosionState() { }
    public void OnTriggerEnter(Collider other) { }
    public void OnTriggerStay(Collider other)
    {
        var explosion = myEnemy.gameObject.GetComponent<EnemyExplosion>();
        if (explosion != null && !myEnemy.isDead)
            explosion.Explode();
    }
    public void OnTriggerExit(Collider other) { }
}

