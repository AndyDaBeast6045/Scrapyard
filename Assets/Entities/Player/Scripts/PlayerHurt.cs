using UnityEngine;

public class PlayerHurt : MonoBehaviour
{
    private PlayerController playerController;
    private void Start()
    {
        playerController = GetComponentInParent<PlayerController>();
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (!playerController.GetColliding())
        {
            if (collision.gameObject.tag == "EnemyAttack" && !playerController.GetDead())
            {
                playerController.TakeDamage(1);
            }
        }
        playerController.SetColliding(true);
    }
}
