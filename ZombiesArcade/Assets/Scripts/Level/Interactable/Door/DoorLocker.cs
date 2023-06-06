using UnityEngine;
using UnityEngine.Events;

public class DoorLocker : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] private KeySO key;
    [SerializeField] private InventorySO playerInventory;

    [Header("Broadcasting on")]
    [SerializeField] private UnityEvent OnDoorUnlocked;

    public void UnlockDoor()
    {
        if (playerInventory != null)
        {
            if (playerInventory.ContainsKey(key))
            {
                OnDoorUnlocked?.Invoke();
                playerInventory.RemoveKey(key);
            }
        }
    }

}
