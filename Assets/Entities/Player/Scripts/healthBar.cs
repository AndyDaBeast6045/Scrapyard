using System;
using UnityEngine;
using UnityEngine.UI;

public class healthBar : MonoBehaviour
{
    public Image healthBarImage;  

    [Range(0f, 100f)]
    private float displayedHP;
    public float hp = 100f; 
    private float initialWidth;
    public float animationSpeed = 5f; 


    private void Start()
    {
        initialWidth = healthBarImage.rectTransform.sizeDelta.x;
        Debug.Log(initialWidth);
        displayedHP = hp;
        UpdateHealthBar();
    }

    private void Update()
    {
        //Just for testing - press H to lower hp by 10, G to increase
        if (Input.GetKeyDown(KeyCode.H))
        {
            hp -= 10f;
            hp = Mathf.Clamp(hp, 0f, 100f);
            UpdateHealthBar();
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            hp += 10f;
            hp = Mathf.Clamp(hp, 0f, 100f);
            UpdateHealthBar();
        }
        if (Mathf.Abs(displayedHP - hp) > 0.01f)
        {
            displayedHP = Mathf.Lerp(displayedHP, hp, Time.deltaTime * animationSpeed);
            UpdateHealthBarAnimated();
        }
    }

    void UpdateHealthBar()
    {
        float hpPercent = hp / 100f;
        healthBarImage.rectTransform.sizeDelta = new Vector2(initialWidth * hpPercent, healthBarImage.rectTransform.sizeDelta.y);
    }
    void UpdateHealthBarAnimated()
    {
        float percent = displayedHP / 100f;
        healthBarImage.rectTransform.sizeDelta = new Vector2(initialWidth * percent, healthBarImage.rectTransform.sizeDelta.y);
    }


    //call this for hp change
    public void updateHealth(float hpChange)
    {

        hp += hpChange;
        hp = Mathf.Clamp(hp, 0f, 100f);
        UpdateHealthBar();

    }








}
