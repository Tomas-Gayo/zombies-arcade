using System.Collections.Generic;
using UnityEngine;

public class DecalControl : MonoBehaviour
{
    // Queues of the decal for the pool and the active objects in scene
    private Queue<GameObject> decalsInPool;
    private Queue<GameObject> decalsInScenePool;

    [Tooltip("The maximum number of decall on scene, after this this number the first one instantiated will be destroyed.")]
    public int maxDecals = 10;
    [Tooltip("This is the prefab of the bullet hole it will be instantiated.")]
    public GameObject decalPrefab;

    private void Awake()
    {
        // All the queues are initialized at the beggining
        Initialize();
    }
    private void Initialize()
    {
        decalsInPool = new Queue<GameObject>();
        decalsInScenePool = new Queue<GameObject>();

        // Fill the queue pool with decals up to maximum decals number
        for (int i = 0; i < maxDecals; i++)
        {
            Instantiate();
        }
    }

    private void Instantiate()
    {
        GameObject gob = Instantiate(decalPrefab);
        // Add the objects in the queue
        decalsInPool.Enqueue(gob);
        // For the moment the objects will be inactive 
        gob.SetActive(false);
    }

    public void Activate(RaycastHit hit)
    {
        // Get next available decal
        GameObject decal = GetNextAvailable();
        if (decal != null)
        {
            // Set position and rotation
            decal.transform.SetPositionAndRotation(hit.point + hit.normal * 0.01f, Quaternion.FromToRotation(Vector3.forward, -hit.normal));

            // Active the object now
            decal.SetActive(true);

            // Add element at the end of the queue
            decalsInScenePool.Enqueue(decal);
        }
    }

    private GameObject GetNextAvailable()
    {
        // If there is something in the pool ...
        if (decalsInPool.Count > 0)
            // ... get and remove the first element on the pool
            return decalsInPool.Dequeue();
        // if pool is empty use the prefabs already in scene
        var oldestActiveDecal = decalsInScenePool.Dequeue();
        return oldestActiveDecal;
    }
}
