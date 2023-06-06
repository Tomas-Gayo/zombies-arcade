using UnityEngine;

[CreateAssetMenu(fileName = "NewKey", menuName = "Scriptable Objects/Key")]
public class KeySO : ScriptableObject
{
    [SerializeField] private string keyName;

    public string KeyName => keyName;
}
