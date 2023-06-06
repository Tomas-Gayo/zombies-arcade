using UnityEngine;
using ScriptableObjectArchitecture;

public class PickKey : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] private KeySO key;

    [Header("Broadcasting On")]
    [SerializeField] private KeySORequestGameEvent keyRequest;

    public void PickTheKey()
    {
        var request = new KeySORequest(keySO:key);

        keyRequest?.Raise(request);

        Destroy(gameObject);
    }
}
