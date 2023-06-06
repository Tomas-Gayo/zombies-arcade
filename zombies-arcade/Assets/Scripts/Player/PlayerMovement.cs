using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float jumpPower;
    public float gravity;
    public float stepDown;
    public float airControl;
    public float airSpeed;
    public float runSpeed;

    private Vector2 input;
    private Vector3 rootMotion;
    private Animator animator;
    private CharacterController characterController;

    private Vector3 velocity;
    private bool isJumping;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        PlayerInput();
        CharacterAnimations();
    }

    private void PlayerInput()
    {
        input.x = Input.GetAxis("Horizontal");
        input.y = Input.GetAxis("Vertical"); 

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        UpdateIsRunning();
    }

    private void UpdateIsRunning()
    {
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        animator.SetBool("isRunning", isRunning);
    }

    private void CharacterAnimations()
    {
        animator.SetFloat("InputX", input.x);
        animator.SetFloat("InputY", input.y);
    }

    // Saves the delta position of the character when moves 
    private void OnAnimatorMove()
    {
        rootMotion += animator.deltaPosition;
    }

    private void FixedUpdate()
    {
        if (isJumping) // Air
        {
            UpdateInAir();
        }
        else // Ground
        {
            UpdateInGround();
        }
    }

    private void UpdateInGround()
    {
        Vector3 stepForwardAmount = rootMotion * runSpeed;
        Vector3 stepDownAmount = Vector3.down * stepDown;
        characterController.Move(stepForwardAmount + stepDownAmount);
        // Force the animation to run on Fixed update -> This is to fix the siticky movement 
        characterController.Move(rootMotion);
        rootMotion = Vector3.zero;
        
        if (characterController.isGrounded)
        {
            SetInAir(0);
        }
    }

    private void UpdateInAir()
    {
        velocity.y -= gravity * Time.fixedDeltaTime;
        Vector3 displacement = velocity * Time.fixedDeltaTime;
        displacement += CalculateAirControl();
        characterController.Move(displacement);
        isJumping = !characterController.isGrounded;
        rootMotion = Vector3.zero;
        animator.SetBool("isJumping", isJumping);
    }

    private Vector3 CalculateAirControl()
    {
        return ((transform.forward * input.y) + (transform.right * input.x)) * (airControl / 100);
    }

    private void Jump()
    {
        if (!isJumping)
        {
            float jumpVelocity = Mathf.Sqrt(2 * gravity * jumpPower);
            SetInAir(jumpVelocity);
        }
    }

    private void SetInAir(float jumpVelocity)
    {
        isJumping = true;
        velocity = airSpeed * runSpeed * animator.velocity;
        velocity.y = jumpVelocity;
    }
}
