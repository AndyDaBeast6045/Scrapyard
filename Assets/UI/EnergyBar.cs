using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{
    private GameObject playerObject;
    private PlayerController playerController;
    [SerializeField] Slider energyBar;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerObject = GameObject.Find("Player");
        playerController = playerObject.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        energyBar.value = (float)playerController.getCurrentStamina() / (float)playerController.getMaxStamina();
    }
}
