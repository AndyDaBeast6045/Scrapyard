using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{
    private PlayerController playerController;
    [SerializeField] Slider energyBar;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        energyBar.value = (float)playerController.getCurrentStamina() / (float)playerController.getMaxStamina();
    }
}
