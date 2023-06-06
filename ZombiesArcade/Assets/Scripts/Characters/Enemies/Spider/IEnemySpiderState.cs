using UnityEngine;

public interface IEnemySpiderState
{
    void UpdateState();
    void GoToChaseState();
    void GoToExplosionState();
    void OnTriggerEnter(Collider other);
    void OnTriggerStay(Collider other);
    void OnTriggerExit(Collider other);
}
