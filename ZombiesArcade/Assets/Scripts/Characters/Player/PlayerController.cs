using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(PlayerInputs))]
public class PlayerController : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] private float walkSpeed;
    [SerializeField, Range(0, 50)] private float sensibilityCameraX;
    [SerializeField, Range(0, 50)] private float sensibilityCameraY;
    [SerializeField] private CinemachineVirtualCamera playerVC;

    [SerializeField] private float gravity;
    [SerializeField] private float jumpHeight;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance;
    [SerializeField] private LayerMask groundLayer;

    // Player physics
    private Vector3 velocity;
    // Camera Config
    private float xRotation;
    private float yRotation;
    // Private dependencies
    private CharacterController controller;
    private AudioSource audioSource;
    private PlayerInputs input;

    private void Start()
    {
        // Dependencies
        controller = GetComponent<CharacterController>();
        audioSource = GetComponent<AudioSource>();
        input = GetComponent<PlayerInputs>();

        // Cursor locked in the middle of the screen
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void Update()
    {
        Look();
        Move();
        Jump();
        Gravity();
    }

    private void Move()
    {
        Vector3 move = transform.right * input.move.x + transform.forward * input.move.y;
        controller.Move(Time.deltaTime * walkSpeed * move);
    }

    private void Look()
    {
        float mouseX = Time.deltaTime * sensibilityCameraX * input.look.x;
        float mouseY = Time.deltaTime * sensibilityCameraY * input.look.y;

        yRotation += mouseX;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90.0f, 90.0f);

        transform.localRotation = Quaternion.Euler(0, yRotation, 0);
        playerVC.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
    }

    private void Jump()
    {
        if (input.jump && Grounded())
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2.0f * gravity);
        }
    }

    private void Gravity()
    {
        if (Grounded() && velocity.y < 0)
            velocity.y = -2;

        velocity.y += Time.deltaTime * gravity;
        controller.Move(Time.deltaTime * velocity);
    }

    private bool Grounded()
    { 
        return Physics.CheckSphere(groundCheck.position, groundDistance, groundLayer); ;
    }
}
