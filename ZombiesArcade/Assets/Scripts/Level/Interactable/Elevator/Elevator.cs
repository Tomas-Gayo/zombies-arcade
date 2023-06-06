using System.Collections;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [Tooltip("Speed of the elevator to be opened/closed")]
    public float speed = 1f;
    [Tooltip("How much will the elevator move.")]
    public float elevationAmount = 1.9f;
    [Tooltip("Transform of default position.")]
    public Transform startPosition;
    [Tooltip("If the elevator is up (true) or down (false). (Useful to define a default state.")]
    public bool isUp = false;    
    
    [HideInInspector]
    public bool isMoving = false;                    // Is the elevator moving. Setted to true if so

    private Vector3 slideDirDown= Vector3.down;         // Vector to slide right direction the door

    private Vector3 startPos;                       // Start posiition

    private Coroutine AnimationCoroutine;

    private void Awake()
    {
        startPos = startPosition.position;
    }

    public void GoingUp()
    {
        if (AnimationCoroutine != null)
        {
            StopCoroutine(AnimationCoroutine);
        }

        AnimationCoroutine = StartCoroutine(ElevatorGoingUp());
    }

    public void GoingDown()
    {
        if (AnimationCoroutine != null)
        {
            StopCoroutine(AnimationCoroutine);
        }

        AnimationCoroutine = StartCoroutine(ElevatorGoingDown());
    }

    public IEnumerator IsMoving()
    {
        yield return new WaitForSeconds(3f);

        isMoving = false;
    }
    private IEnumerator ElevatorGoingUp()
    {
        Vector3 endPosition = startPosition.position;
        float time = 0;
        isUp = true;
        isMoving = true;
        while (time < 1)
        {
            startPosition.position = Vector3.Lerp(endPosition, startPos, time);
            yield return null;
            time += Time.deltaTime * speed;
        }
    }

    private IEnumerator ElevatorGoingDown()
    {
        Vector3 endPosition = startPosition.position + elevationAmount * slideDirDown;
        float time = 0;
        isUp = false;
        isMoving = true;
        while (time < 1)
        {
            startPosition.position = Vector3.Lerp(startPos, endPosition, time);
            yield return null;
            time += Time.deltaTime * speed;
        }
    }
}
