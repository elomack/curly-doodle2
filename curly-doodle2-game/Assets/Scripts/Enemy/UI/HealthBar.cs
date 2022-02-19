using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    private Text healthText;

    private void Start()
    {
        healthText = GetComponentInChildren<Text>();
    }

    public void SetHealth(float health){
        slider.value = health;
    }

    public void SetMaxHealth(float health){
        slider.maxValue = health;
        slider.value = health;
    }

    public void SetHealthBarText(float currentHealth, float maxHealth)
    {
        float healthPercent = Mathf.Round(currentHealth / maxHealth * 100f);
        healthText.text = Mathf.Clamp(currentHealth,0f,maxHealth) + " / " + maxHealth
            + " (" + Mathf.Clamp(healthPercent, 0f, 100f) + "%) ";
    }
}
