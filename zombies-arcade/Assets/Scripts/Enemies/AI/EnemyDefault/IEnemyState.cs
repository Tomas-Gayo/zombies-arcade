using UnityEngine;

public interface IEnemyState
{
    void UpdateState();
    void GoToIdleState();
    void GoToChaseState();
    void GoToAttackState();
    void OnTriggerEnter(Collider other);
    void OnTriggerStay(Collider other);
    void OnTriggerExit(Collider other);

}
