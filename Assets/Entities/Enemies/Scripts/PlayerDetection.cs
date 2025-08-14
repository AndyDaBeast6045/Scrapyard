using System.Collections;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    private EnemyController enemyController;
    [SerializeField] private Animator animationController;

    private void Start()
    {
        if (enemyController == null)
        {
            enemyController = GetComponentInParent<EnemyController>();
        }
    }
    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(Timer());
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            animationController.SetBool("Attack", false);
        }
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(0.5f);
        if (enemyController.GetBoss())
        {
            if (Random.Range(0f, 1f) <= 0.5f)
            {
                animationController.SetTrigger("Heavy");
            }
            else
            {
                animationController.SetBool("Attack", true);
                yield return new WaitForSeconds(0.5f);
                animationController.SetBool("Attack", false);
            }
        }
        else
        {
            animationController.SetBool("Attack", true);
            yield return new WaitForSeconds(0.5f);
            animationController.SetBool("Attack", false);
        }
        animationController.ResetTrigger("Attack");
        animationController.ResetTrigger("Heavy");
    }
}
