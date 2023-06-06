using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerChecker : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] private string tagName;

    [Header("Events")]
    [SerializeField] private UnityEvent OnEnter;
    [SerializeField] private UnityEvent OnStay;
    [SerializeField] private UnityEvent OnExit;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(tagName))
            OnEnter!.Invoke();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(tagName))
            OnStay!.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(tagName))
            OnExit!.Invoke();
    }
}
