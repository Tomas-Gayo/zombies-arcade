using UnityEngine;

public class Key : MonoBehaviour
{
    [Tooltip("Speed of the key rotation when is spawned in the spaceworld.")]
    public float speedRotation = 2f;

    [SerializeField] private KeyType keyType;
    public enum KeyType
    {
        Entrance, 
        Garden,
        Final,
        None
    }

    private void FixedUpdate()
    {
        transform.Rotate(0, speedRotation, 0, Space.World);
    }

    public KeyType GetKeyType()
    {
        return keyType;
    }
}
