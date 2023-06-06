using UnityEngine;

public class EnemyDrop : MonoBehaviour
{
    [Tooltip("Object that the enemy is going to drop on dead.")]
    public GameObject[] dropPrefab;
    [Tooltip("Offset on the y axis to adjust the position of the object spawned.")]
    public Vector3 offset;

    public void DropObject()
    {
        int randomValue = Random.Range(0, dropPrefab.Length);
        // spawn random object of the array
        Instantiate(dropPrefab[randomValue], transform.position + offset, Quaternion.identity);
    }
}
