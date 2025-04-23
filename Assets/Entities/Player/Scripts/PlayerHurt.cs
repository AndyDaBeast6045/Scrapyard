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
        if (playerController.GetColliding() == false)
        {
            if (collision.gameObject.tag == "EnemyAttack" && !playerController.GetDead())
            {
                playerController.TakeDamage(1);
            }
            else if (collision.gameObject.tag == "EnemyHeavy" && !playerController.GetDead())
            {
                playerController.TakeDamage(5);
            }
            playerController.SetColliding(true);
        }
    }
}
