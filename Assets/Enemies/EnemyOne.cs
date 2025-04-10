using UnityEngine;

public class EnemyOne : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private float colliderDistance;
    [SerializeField] private int damage;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask playerLayer;
    private float cooldownTimer = Mathf.Infinity;
    private EnemyOnePatrol enemyPatrol;
    private bool isShielded = false;
    [SerializeField] private float shieldDuration;
    private float shieldTimer;
    private float damageTemp;


    //private Health playerHealth; (Eshaan)

    private void Awake() {
       enemyPatrol = GetComponentInParent<EnemyOnePatrol>();
    }

    void Update() {
        cooldownTimer += Time.deltaTime;

        if (PlayerInSight()) {
            if (!isShielded) {
                if (cooldownTimer >= attackCooldown) {
                    // playerHealth.TakeDamage(damage);
                    cooldownTimer = 0;
                    isShielded = true;
                }
            } else {
                Shield();
            }
        }

        if (enemyPatrol != null) {
            enemyPatrol.enabled = !PlayerInSight();
        }
    }

    private void Shield() {
        shieldTimer += Time.deltaTime;
        // damageTemp = playerDamage;
        // playerDamage = 0;
        if (shieldTimer > shieldDuration) {
            isShielded = !isShielded;
            // playerDamage = damageTemp;
        }
    }

    private bool PlayerInSight() {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, 
        new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z), 0, Vector2.left, 0, playerLayer);

        if (hit.collider != null) {
        //    playerHealth = hit.transform.GetComponent<Health done by Eshaan>();
        }

        return hit.collider != null;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, 
        new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }
}
