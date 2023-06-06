using UnityEngine;

public class ElevatorTrigger : MonoBehaviour
{
    [Tooltip("Reference to the script of the door so it is possible to open and close doors.")]
    public Elevator elevator;

    private void OnTriggerEnter(Collider other)
    {
        /**
        if (!elevator.isUp && !elevator.isMoving)
        {
            elevator.GoingUp();
            StartCoroutine(elevator.IsMoving());
        }
        */
        // for the moment we want only to go down
        if (elevator.isUp && !elevator.isMoving)
        {
            elevator.GoingDown();
            StartCoroutine(elevator.IsMoving());
        }
    }
}
