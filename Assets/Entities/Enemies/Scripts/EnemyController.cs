using System.Collections;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyController : MonoBehaviour
{
    private GameObject playerObject = null;
    
    private Vector2 playerPosition;
    private int xMovement = 0;
    private float yMovement = 0;
    private bool isColliding = false;
    private bool fastEnemy = false;
    private HitCounter hitCounter = null;
    private bool bossEnemy = false;

    [SerializeField] private bool dead = false;
    [SerializeField] private int health = 5;
    [SerializeField] private float moveSpeed = 0.75f;
    [SerializeField] private bool moveEnabled = true;
    [SerializeField] private Collider detection;
    [SerializeField] private Animator animationController;
    [SerializeField] private SpriteRenderer spriteRenderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (hitCounter == null)
        {
            hitCounter = GameObject.Find("HitCounter").GetComponent<HitCounter>();
        }
        if (playerObject == null)
        {
            playerObject = GameObject.Find("Player");
        }
        spriteRenderer.color = Color.red;
        //SetBoss();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (dead)
        {
            Destroy(gameObject);
        }
        SetColliding(false);
        playerPosition = new Vector2(playerObject.transform.position.x, playerObject.transform.position.y);
        if (moveEnabled)
        {
            if (playerPosition.x > transform.position.x)
            {
                xMovement = 1;
            }
            else if (playerPosition.x < transform.position.x)
            {
                xMovement = -1;
            }
            else
            {
                xMovement = 0;
            }
            if (playerPosition.y > transform.position.y)
            {
                yMovement = 0.6f;
            }
            else if (playerPosition.y < transform.position.y)
            {
                yMovement = -0.6f;
            }
            else
            {
                yMovement = 0;
            }
            if (xMovement > 0)
            {
                transform.rotation = new Quaternion(0, 0, 0, 0);
            }
            else if (xMovement < 0)
            {
                transform.rotation = new Quaternion(0, 180, 0, 0);
            }
            transform.position += new Vector3(xMovement, yMovement, yMovement) * moveSpeed * Time.deltaTime;
        }
    }
    public void TakeDamage(int damage, string type)
    {
        animationController.SetTrigger("Damage");
        health -= damage;
        hitCounter.isHit = true;
        hitCounter.attackType = type;
        if (health <= 0)
        {
            animationController.SetTrigger("Death");
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

    public bool GetFast()
    {
        return fastEnemy;
    }

    public bool GetBoss()
    {
        return bossEnemy;
    }

    public void EnemyDifficultyRange(float ratio)
    {
        if (Random.Range(0f, 1f) >= ratio)
        {
            fastEnemy = true;
            moveSpeed = 1.25f;
            animationController.speed = 1.25f;
            spriteRenderer.color = Color.blue;
        }
    }

    public void SetBoss()
    {
        bossEnemy = true;
        health = 100;
        moveSpeed = 1f;
        spriteRenderer.color = new Color(1, 0, 1);
    }
}
