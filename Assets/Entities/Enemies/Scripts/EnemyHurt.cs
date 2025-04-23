using UnityEngine;

public class EnemyHurt : MonoBehaviour
{
    private EnemyController enemyController;

    void Start()
    {
        enemyController = GetComponentInParent<EnemyController>();
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (enemyController.GetColliding() == false)
        {
            if (collision.gameObject.tag == "PlayerLight")
            {
                enemyController.TakeDamage(1, "light");
            }
            else if (collision.gameObject.tag == "PlayerHeavy")
            {
                enemyController.TakeDamage(10, "heavy");
            }
            enemyController.SetColliding(true);
        }
    }
}
