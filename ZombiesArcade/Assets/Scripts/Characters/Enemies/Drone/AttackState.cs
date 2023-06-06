using UnityEngine;

public class AttackState : IEnemyDroneState
{
    EnemyDroneAI myEnemy;
    private float actualTimeBetweenShoots = 0;

    // Save reference of the enemy AI
    public AttackState(EnemyDroneAI enemy)
    {
        myEnemy = enemy;
    }

    public void UpdateState()
    {
        myEnemy.myLight.color = Color.red;
        actualTimeBetweenShoots += Time.deltaTime;
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
        myEnemy.currentState = myEnemy.chaseState;
    }


    public void OnTriggerEnter(Collider other) { }

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) {

            Vector3 lookDirection =
                other.transform.position - myEnemy.transform.position;

            myEnemy.transform.rotation =
                Quaternion.FromToRotation(Vector3.forward,
                new Vector3(lookDirection.x, 0, lookDirection.z));

            if (actualTimeBetweenShoots > myEnemy.timeBetweenShoots)
            {
                actualTimeBetweenShoots = 0;

                var gun = myEnemy.gameObject.GetComponent<EnemyShooter>();
                if (gun != null)
                    gun.Shoot();
            }

            float distance = Vector3.Distance(myEnemy.transform.position, other.transform.position);
            if (distance > myEnemy.shotRange)
            {
                GoToChaseState();
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            GoToAlertState();
    }
}
