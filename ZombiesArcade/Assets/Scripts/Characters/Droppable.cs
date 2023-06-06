using UnityEngine;

public class Droppable : MonoBehaviour
{
    [Tooltip("Drop objects selected randomly, add only one object to avoid the randomization.")]
    [SerializeField] private GameObject[] dropPrefab;
    [Tooltip("Offset on the y axis to adjust the position of the object spawned.")]
    [SerializeField] private Vector3 offset;
    [SerializeField] private Transform dropParent;

    public void DropObject()
    {
        if (dropPrefab.Length <= 0)
            return;

        int randomValue = Random.Range(0, dropPrefab.Length);
        
        if (dropPrefab[randomValue] != null)
        {
            GameObject drop;
            drop = Instantiate(dropPrefab[randomValue], transform.position + offset, dropPrefab[randomValue].transform.rotation);
            drop.transform.parent = dropParent;
        }
    }
}
