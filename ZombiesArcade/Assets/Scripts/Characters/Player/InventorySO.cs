using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewInventory", menuName = "Scriptable Objects/Inventory")]
public class InventorySO : ScriptableObject
{
    [SerializeField] private List<KeySO> keysInventory;

    private void OnEnable()
    {
        keysInventory = new List<KeySO>();
    }
    public void AddKey(KeySO key)
    {
        keysInventory.Add(key);
    }

    public void RemoveKey(KeySO key)
    {
        if (keysInventory.Contains(key))
            keysInventory.Remove(key);
    }

    public bool ContainsKey(KeySO key)
    {
        return keysInventory.Contains(key);
    }
}
