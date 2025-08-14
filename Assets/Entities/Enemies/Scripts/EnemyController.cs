using System.Collections;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    private SpawnerManager spawnerManager = null;
    private int health = 5;
    private float moveSpeed = 0.75f;

    [SerializeField] private bool dead = false; 
    [SerializeField] private bool moveEnabled = true;
    [SerializeField] private Collider detection;
    [SerializeField] private Animator animationController;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip woosh;
    [SerializeField] private AudioClip hit;
    [SerializeField] private AudioClip heavy;
    [SerializeField] private AudioClip twinkle;
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
        if (spawnerManager == null)
        {
            spawnerManager = GameObject.Find("Spawner").GetComponent<SpawnerManager>();
        }
        health = 5;
        moveSpeed = 0.75f;
        animationController.speed = 1f;
        spriteRenderer.color = Color.red;
        if (spawnerManager.GetSpawned() == 30)
        {
            SetBoss();
        }
        else
        {
            EnemyDifficultyRange(spawnerManager.GetRatio());
        }
        
    }
    public void PlayWoosh()
    {
        audioSource.PlayOneShot(woosh, 0.5f);
    }
    public void PlayHit()
    {
        audioSource.PlayOneShot(hit, 0.5f);
    }
    public void PlayHeavy()
    {
        audioSource.PlayOneShot(heavy, 0.5f);
    }
    public void PlayTwinkle()
    {
        audioSource.PlayOneShot(twinkle, 0.5f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if (dead)
        {
            Destroy(gameObject);
        }
        SetColliding(false);
        playerPosition = new Vector2(playerObject.transform.position.x, playerObject.transform.position.y);
        if (moveEnabled)
        {
            if (fastEnemy)
            {
                animationController.speed = 1.25f;
            }
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
            transform.position += new Vector3(xMovement, yMovement, yMovement) * moveSpeed * Time.fixedDeltaTime;
        }
    }
    public void TakeDamage(int damage, string type)
    {
        animationController.speed = 1f;
        SetColliding(true);
        if (bossEnemy)
        {
            PlayHit();
        }
        else
        {
            animationController.ResetTrigger("Damage");
            animationController.SetTrigger("Damage");
        }
        health -= damage;
        hitCounter.isHit = true;
        hitCounter.attackType = type;
        if (health <= 0)
        {
            animationController.SetTrigger("Death");
            if(bossEnemy && !spawnerManager.GetEndless())
            {
                StartCoroutine(DieCoroutine());
            }
        }
    }

    IEnumerator DieCoroutine()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("ResultsScreenTesting");
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
        if (Random.Range(0f, 1f) <= ratio)
        {
            health = 10;
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
        animationController.speed = 1f;
        moveSpeed = 1f;
        spriteRenderer.color = new Color(1, 0, 1);
    }
}
