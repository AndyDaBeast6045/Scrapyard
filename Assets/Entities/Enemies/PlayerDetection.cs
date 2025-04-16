using System.Collections;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    [SerializeField] private Animator animationController;

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
        animationController.SetBool("Attack", true);
        yield return new WaitForSeconds(0.5f);
        animationController.SetBool("Attack", false);
    }
}
