using System;
using UnityEngine;
using UnityEngine.UI;

public class staminaBar : MonoBehaviour
{
    public Image healthBarImage;  // Assign in Inspector (UI Image component)

    [Range(0f, 100f)]
    private float displayedHP;
    public float stam = 100f; // Temporary HP value (0 to 100)
    private float initialWidth;
    public float animationSpeed = 5f; // Adjust to make it faster/slower


    private void Start()
    {
        initialWidth = healthBarImage.rectTransform.sizeDelta.x;
        Debug.Log(initialWidth);
        displayedHP = stam;
        UpdateHealthBar();
    }

    private void Update()
    {


        //Just for testing 
        if (Input.GetKeyDown(KeyCode.Q))
        {
            stam -= 10f;
            stam = Mathf.Clamp(stam, 0f, 100f);
            UpdateHealthBar();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            stam += 10f;
            stam = Mathf.Clamp(stam, 0f, 100f);
            UpdateHealthBar();
        }

        //Keep this in update
        stam += .05f;
        stam = Mathf.Clamp(stam, 0f, 100f);
        UpdateHealthBar();
        if (Mathf.Abs(displayedHP - stam) > 0.01f)
        {
            displayedHP = Mathf.Lerp(displayedHP, stam, Time.deltaTime * animationSpeed);
            UpdateHealthBarAnimated();
        }

    }

    void UpdateHealthBar()
    {
        float hpPercent = stam / 100f;
        healthBarImage.rectTransform.sizeDelta = new Vector2(initialWidth * hpPercent, healthBarImage.rectTransform.sizeDelta.y);
    }
    void UpdateHealthBarAnimated()
    {
        float percent = displayedHP / 100f;
        healthBarImage.rectTransform.sizeDelta = new Vector2(initialWidth * percent, healthBarImage.rectTransform.sizeDelta.y);
    }


    //call this for stamina change
    public void updateStam(float stamChange)
    {

        stam += stamChange;
        stam = Mathf.Clamp(stam, 0f, 100f);
        UpdateHealthBar();

    }









}
