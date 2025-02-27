using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    private InputAction moveAction;
    private InputAction sprintAction;
    private InputAction jumpAction;

    [SerializeField] private float moveSpeed = 1.5f;
    [SerializeField] private float sprintSpeed = 1.5f;
    [SerializeField] private float jumpSpeed = 3.0f;
    [SerializeField] private float staminaMax = 100.0f;
    [SerializeField] private float staminaDrain = 20.0f;
    [SerializeField] private float staminaRegen = 30.0f;
    [SerializeField] private float staminaCurrent;
    [SerializeField] private bool runningEnabled;

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
            transform.position += new Vector3(moveValue.x, 0, 0) * moveSpeed * sprintSpeed * Time.deltaTime;
            transform.position += new Vector3(0, moveValue.y, moveValue.y) * moveSpeed * Time.deltaTime;
            staminaCurrent -= staminaDrain * Time.deltaTime;
            if (staminaCurrent < 0)
            {
                runningEnabled = false;
            }
        }
        else
        {
            transform.position += new Vector3(moveValue.x, moveValue.y, moveValue.y) * moveSpeed * Time.deltaTime;
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
    }
}
