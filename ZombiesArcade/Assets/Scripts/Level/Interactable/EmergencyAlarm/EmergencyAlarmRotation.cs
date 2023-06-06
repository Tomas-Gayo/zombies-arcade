using UnityEngine;

public class EmergencyAlarmRotation : MonoBehaviour
{
    [Tooltip("Speed of rotation of the light.")]
    public float speedRotation = 2f;

    private void FixedUpdate()
    {
        transform.Rotate(0, speedRotation, 0, Space.Self);
    }
}
