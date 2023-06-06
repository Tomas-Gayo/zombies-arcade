using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputs : MonoBehaviour
{
    [HideInInspector] public Vector2 move;
    [HideInInspector] public Vector2 look;
    [HideInInspector] public bool jump;
    [HideInInspector] public bool shoot;
    [HideInInspector] public bool realod;

    public void OnMove(InputAction.CallbackContext value)
    {
        move = value.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext value)
    {
        look = value.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext value)
    {
        jump = value.performed;
    }

    public void OnShoot(InputAction.CallbackContext value)
    {
        shoot = value.performed;
    }

    public void OnReload(InputAction.CallbackContext value)
    {
        realod = value.performed;
    }
}
