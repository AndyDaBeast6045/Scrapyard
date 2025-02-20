using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    private InputAction moveAction;
    private InputAction sprintAction;

    [SerializeField] private float walkSpeed = 2f;
    [SerializeField] private float sprintSpeed = 3.5f;
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
    }

    // Update is called once per frame
    void Update()
    {
        // Getting the movement input
        moveValue = moveAction.ReadValue<Vector2>();

        // Movement
        if (sprintAction.IsPressed() & runningEnabled)
        {
            transform.position += Vector3.Normalize(new Vector3(moveValue.x, moveValue.y, 0)) * sprintSpeed * Time.deltaTime;
            staminaCurrent -= staminaDrain * Time.deltaTime;
            if (staminaCurrent < 0)
            {
                runningEnabled = false;
            }
        }
        else
        {
            transform.position += Vector3.Normalize(new Vector3(moveValue.x, moveValue.y, 0)) * walkSpeed * Time.deltaTime;
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
