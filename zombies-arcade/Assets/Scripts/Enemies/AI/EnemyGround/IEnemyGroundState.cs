using UnityEngine;

public interface IEnemyGroundState
{
    void UpdateState();
    void GoToChaseState();
    void GoToAttackState();
    void OnTriggerEnter(Collider other);
    void OnTriggerStay(Collider other);
    void OnTriggerExit(Collider other);

}
