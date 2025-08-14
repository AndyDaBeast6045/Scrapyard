using System.Collections;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    private InputAction _moveAction;
    private InputAction _sprintAction;
    private InputAction _lightAction;
    private InputAction _heavyAction;
    private bool isColliding = false;
    private bool dead = false;
    private HitCounter hitCounter = null;

    [SerializeField] private int maxHealth = 20;
    [SerializeField] private int currentHealth = 20;
    [SerializeField] private float moveSpeed = 1.5f;
    [SerializeField] private float sprintSpeed = 1.5f;
    [SerializeField] private float staminaMax = 100.0f;
    [SerializeField] private float staminaDrain = 20.0f;
    [SerializeField] private float staminaRegen = 30.0f;
    [SerializeField] private float staminaCurrent;
    [SerializeField] private bool moveEnabled = true;
    [SerializeField] private Animator animationController;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip woosh;
    [SerializeField] private AudioClip hit;
    [SerializeField] private AudioClip heavy;
    [SerializeField] private AudioClip twinkle;

    private Vector2 _moveValue;
    private bool _runningEnabled = true;

    public int getMaxHealth()
    {
        return maxHealth;
    }
    public int getCurrentHealth()
    {
        return currentHealth;
    }
    public float getMaxStamina()
    {
        return staminaMax;
    }
    public float getCurrentStamina()
    {
        return staminaCurrent;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        staminaCurrent = staminaMax;
        _moveAction = InputSystem.actions.FindAction("Move");
        _sprintAction = InputSystem.actions.FindAction("Sprint");
        _lightAction = InputSystem.actions.FindAction("LightAttack");
        _heavyAction = InputSystem.actions.FindAction("HeavyAttack");
        if (hitCounter == null)
        {
            hitCounter = GameObject.Find("HitCounter").GetComponent<HitCounter>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Getting the movement input
        _moveValue = _moveAction.ReadValue<Vector2>();
        //transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y);

        if (_moveValue != new Vector2(0, 0))
        {
            animationController.SetBool("Moving", true);
        }
        else
        {
            animationController.SetBool("Moving", false);
        }
        if (_lightAction.WasPressedThisFrame())
        {
            animationController.SetTrigger("Light");
        }
        if (_heavyAction.WasPressedThisFrame() && (moveEnabled == true))
        {
            staminaCurrent -= 30f;
            animationController.SetTrigger("Heavy");
        }
        SetColliding(false);
        if (moveEnabled)
        {
            if (_moveValue.x > 0)
            {
                transform.rotation = new Quaternion(0, 0, 0, 0);
            }
            else if (_moveValue.x < 0)
            {
                transform.rotation = new Quaternion(0, 180, 0, 0);
            }

            // Movement
            if (_sprintAction.IsPressed() & _runningEnabled)
            {
                transform.position += new Vector3(_moveValue.x, 0, 0) * moveSpeed * sprintSpeed * Time.deltaTime;
                transform.position += new Vector3(0, _moveValue.y, _moveValue.y) * moveSpeed * Time.deltaTime;
                staminaCurrent -= staminaDrain * Time.deltaTime;
                animationController.speed = 2f;
                if (staminaCurrent < 0)
                {
                    _runningEnabled = false;
                }
            }
            else
            {
                transform.position += new Vector3(_moveValue.x, _moveValue.y, _moveValue.y) * moveSpeed * Time.deltaTime;
                animationController.speed = 1;
                if (staminaCurrent < staminaMax)
                {
                    staminaCurrent += staminaRegen * Time.deltaTime;
                }
                if (staminaCurrent >= staminaMax)
                {
                    _runningEnabled = true;
                    staminaCurrent = staminaMax;
                }
            }
        }
        else
        {
            animationController.speed = 1;
        }
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -7.0f, 7.0f), Mathf.Clamp(transform.position.y, -3.5f, 0.3f), Mathf.Clamp(transform.position.y, -3.5f, 0.3f));
    }
    public void TakeDamage(int damage)
    {
        hitCounter.Reset();
        animationController.ResetTrigger("Damaged");
        animationController.SetTrigger("Damaged");
        currentHealth -= damage;
        if (currentHealth <= 0 && !dead)
        {
            animationController.SetTrigger("Death");
            dead = true;
            StartCoroutine(WaitForDeathAnimation());
        }
    }
    public void SetColliding(bool colliding)
    {
        if (colliding)
        {
            isColliding = true;
        }
        else
        {
            isColliding = false;
        }
    }
    public bool GetColliding()
    {
        return isColliding;
    }
    public bool GetDead()
    {
        return dead;
    }

    public void PlayWoosh()
    {
        audioSource.PlayOneShot(woosh, 1f);
    }
    public void PlayHit()
    {
        audioSource.PlayOneShot(hit, 1f);
    }
    public void PlayHeavy()
    {
        audioSource.PlayOneShot(heavy, 1f);
    }
    public void PlayTwinkle()
    {
        audioSource.PlayOneShot(twinkle, 1f);
    }

    IEnumerator WaitForDeathAnimation()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("ResultsScreenTesting");
    }
}