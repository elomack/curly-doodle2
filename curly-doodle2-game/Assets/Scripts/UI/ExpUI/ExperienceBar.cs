using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceBar : MonoBehaviour
{
    private Slider slider;
    private Text experienceText;
    private float exp;
    private float maxExp;

    private void Start()
    {
        slider = GetComponent<Slider>();
        experienceText = GetComponentInChildren<Text>();
    }

    public void SetExperience(float currentExperience, float targetExperience)
    {
        exp = currentExperience;
        maxExp = targetExperience;
        float experiencePercent = Mathf.Round(currentExperience / targetExperience * 100f);
        slider.value = experiencePercent;
        experienceText.text = exp + " / " + maxExp + " (" + experiencePercent + "%) ";
    }
}
