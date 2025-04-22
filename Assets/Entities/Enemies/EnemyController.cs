using System.Collections;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyController : MonoBehaviour
{
    private GameObject playerObject = null;
    private GameObject hitCounter = null;
    private Vector2 playerPosition;
    private int xMovement = 0;
    private float yMovement = 0;
    private bool isColliding = false;
    private bool fastEnemy = false;

    [SerializeField] private bool dead = false;

    [SerializeField] private int health = 5;
    [SerializeField] private float moveSpeed = 0.75f;
    [SerializeField] private bool moveEnabled = true;
    [SerializeField] private Collider detection;
    [SerializeField] private Animator animationController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (playerObject == null)
        {
            playerObject = GameObject.Find("Player");
        }
        if (hitCounter == null)
        {
            hitCounter = GameObject.Find("HitCounter");
        }
        if (Random.Range(0f, 1f) > 0.7f)
        {
            fastEnemy = true;
            moveSpeed = 1.25f;
            animationController.speed = 2;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (dead)
        {
            Destroy(gameObject);
        }
        isColliding = false;
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

    private void OnTriggerEnter(Collider collision)
    {
        if (!isColliding)
        {
            if (collision.gameObject.tag == "PlayerLight")
            {
                animationController.SetTrigger("Damage");
                health -= 1;
                hitCounter.GetComponent<HitCounter>().isHit = true;
                hitCounter.GetComponent<HitCounter>().attackType = "light";
                if (health <= 0)
                {
                    animationController.SetTrigger("Death");
                }
            }
            else if (collision.gameObject.tag == "PlayerHeavy")
            {
                hitCounter.GetComponent<HitCounter>().isHit = true;
                hitCounter.GetComponent<HitCounter>().attackType = "heavy";
                animationController.SetTrigger("Death");
            }
        }
        isColliding = true;
    }
}
