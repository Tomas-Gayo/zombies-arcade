using UnityEngine;

public class AimTarget : MonoBehaviour
{
    Ray ray;
    RaycastHit hit;

    private void Update()
    {
        // Cast a ray from the center of the camera forward
        ray.origin = Camera.main.transform.position;
        ray.direction = Camera.main.transform.forward;
        Physics.Raycast(ray, out hit);
        // Then the object will be in the hit position and we use it as the aim position
        transform.position = hit.point;
    }
}
