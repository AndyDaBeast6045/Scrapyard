using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    private InputAction moveAction;
    private InputAction sprintAction;
    private InputAction jumpAction;

    [SerializeField] private float horizontalSpeed = 3.0f;
    [SerializeField] private float verticalSpeed = 1.5f;
    [SerializeField] private float sprintSpeed = 1.5f;
    [SerializeField] private float jumpSpeed = 3.0f;
    [SerializeField] private float staminaMax = 100.0f;
    [SerializeField] private float staminaDrain = 20.0f;
    [SerializeField] private float staminaRegen = 30.0f;
    [SerializeField] private float staminaCurrent;
    [SerializeField] private bool runningEnabled;
    [SerializeField] private bool jumping;
    [SerializeField] private bool falling;
    [SerializeField] private float jumpHeight;
    

    private Vector2 moveValue;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        staminaCurrent = staminaMax;
        moveAction = InputSystem.actions.FindAction("Move");
        sprintAction = InputSystem.actions.FindAction("Sprint");
        jumpAction = InputSystem.actions.FindAction("Jump");
        jumping = false;
        falling = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Getting the movement input
        moveValue = moveAction.ReadValue<Vector2>();

        // Movement
        if (sprintAction.IsPressed() & runningEnabled)
        {
            transform.position += new Vector3(moveValue.x, 0, 0) * horizontalSpeed * sprintSpeed * Time.deltaTime;
            transform.position += new Vector3(0, moveValue.y, moveValue.y) * verticalSpeed * Time.deltaTime;
            staminaCurrent -= staminaDrain * Time.deltaTime;
            if (staminaCurrent < 0)
            {
                runningEnabled = false;
            }
        }
        else
        {
            transform.position += new Vector3(moveValue.x, 0, 0) * horizontalSpeed * Time.deltaTime;
            transform.position += new Vector3(0, moveValue.y, moveValue.y) * verticalSpeed * Time.deltaTime;
            if (staminaCurrent < staminaMax)
            {
                staminaCurrent += staminaRegen * Time.deltaTime;
            }
            if (staminaCurrent >= staminaMax)
            {
                runningEnabled = true;
                staminaCurrent = staminaMax;
            }
        }

        // Jumping
        if (jumpAction.IsPressed() & !jumping)
        {
            jumping = true;
        }
        if (jumping)
        {
            transform.position += new Vector3(0, 0, 1) * jumpSpeed;
        }
        if (transform.position.z > jumpHeight)
        {
            jumping = false;
            falling = true;
        }
        if (falling == true)
        {
            transform.position -= new Vector3(0, 0, 1) * jumpSpeed;
        }
        if (transform.position.z < transform.position.y)
        {
            falling = false;
        }
    }
}
