using UnityEngine;

public class RotateObject : MonoBehaviour
{
    [Tooltip("Rotation speed of the box.")]
    [SerializeField] private float speedRotation;

    private void FixedUpdate()
    {
        transform.Rotate(0, speedRotation, 0, Space.World);
    }
}
