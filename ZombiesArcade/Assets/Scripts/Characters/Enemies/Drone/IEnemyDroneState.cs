using UnityEngine;

public interface IEnemyDroneState
{
    void UpdateState();
    void GoToPatrolState();
    void GoToAlertState();
    void GoToChaseState();
    void GoToAttackState();
    void OnTriggerEnter(Collider other);
    void OnTriggerStay(Collider other);
    void OnTriggerExit(Collider other);
    void Impact();
}
