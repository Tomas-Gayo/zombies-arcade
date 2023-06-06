using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] private float doorSpeed;
    [SerializeField] private float slideAmount = 1.9f;
    [SerializeField] private Transform startPositionRightDoor;
    [SerializeField] private Transform startPositionLeftDoor;
    [Tooltip("True if requires a key, false if not.")]
    [SerializeField] private bool canOpen;

    private Vector3 startPosRight;                  // Start posiition of the right door
    private Vector3 startPosLeft;                   // Start posiition of the left door

    // Private references
    private bool isOpen;
    private Coroutine AnimationCoroutine;

    private void Awake()
    {
        startPosRight = startPositionRightDoor.localPosition;
        startPosLeft = startPositionLeftDoor.localPosition;
    }

    public void Open()
    {
        if (!canOpen)
            return;

        if (!isOpen)
        {
            if (AnimationCoroutine != null)
            {
                StopCoroutine(AnimationCoroutine);
            }

            AnimationCoroutine = StartCoroutine(SlidingOpen());
        }
    }

    public void Close()
    {
        if (isOpen)
        {
            if (AnimationCoroutine != null)
            {
                StopCoroutine(AnimationCoroutine);
            }

            AnimationCoroutine = StartCoroutine(SlidingClose());
        }
    }

    public void UnlockDoor()
    {
        canOpen = true;
    }

    private IEnumerator SlidingOpen()
    {
        Vector3 endPositionRight = startPositionRightDoor.localPosition + slideAmount * Vector3.right;
        Vector3 endPositionLeft = startPositionLeftDoor.localPosition + slideAmount * Vector3.left; ;

        float time = 0;
        isOpen = true;
        while (time < 1)
        {
            startPositionRightDoor.localPosition = Vector3.Lerp(startPosRight, endPositionRight, time);
            startPositionLeftDoor.localPosition = Vector3.Lerp(startPosLeft, endPositionLeft, time);
            yield return null;
            time += Time.deltaTime * doorSpeed;
        }
    }

    private IEnumerator SlidingClose()
    {
        Vector3 endPositionRight = startPositionRightDoor.localPosition;
        Vector3 endPositionLeft = startPositionLeftDoor.localPosition;

        float time = 0;
        isOpen = false;
        while (time < 1)
        {
            startPositionRightDoor.localPosition = Vector3.Lerp(endPositionRight, startPosRight, time);
            startPositionLeftDoor.localPosition = Vector3.Lerp(endPositionLeft, startPosLeft, time);
            yield return null;
            time += Time.deltaTime * doorSpeed;
        }
    }
}
