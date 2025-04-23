using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private PlayerController playerController;
    [SerializeField] Slider healthBar;
    [SerializeField] Image border;
    [SerializeField] Image fill;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.value = (float)playerController.getCurrentHealth() / (float)playerController.getMaxHealth();
        if (healthBar.value > 0.6)
        {
            border.color = Color.green;
            fill.color = Color.green;
        }
        else if (healthBar.value > 0.3)
        {
            border.color = Color.yellow;
            fill.color = Color.yellow;
        }
        else
        {
            border.color = Color.red;
            fill.color = Color.red;
        }
    }
}
