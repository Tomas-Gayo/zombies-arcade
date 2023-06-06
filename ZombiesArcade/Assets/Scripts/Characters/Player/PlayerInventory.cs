using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] private InventorySO inventory;

    public void AddKey(KeySORequest request)
    {
        if (!inventory.ContainsKey(request.keySO))
            inventory.AddKey(request.keySO);
    }
}
