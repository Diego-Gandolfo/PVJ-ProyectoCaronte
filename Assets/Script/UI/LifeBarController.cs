using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeBarController : MonoBehaviour
{
    [SerializeField] private GameObject lifeBar;
    [SerializeField] private Image lifeBarImage;
    [SerializeField] private Text percentage;

    private float currentHealth;
    private float uiHealth;
    private float currentLerpTime;

    private Color32 initialColor;

    public float MaxHealth { get; set; }
    public bool IsVisible { get; private set; }

    private void Start()
    {
        var healthController = GetComponent<HealthController>();
        if (healthController != null)
        {
            MaxHealth = healthController.MaxHealth;
            uiHealth = MaxHealth;
            currentHealth = uiHealth;
        }

        SetBarColorsValues();
    }

    private void SetBarColorsValues()
    {
        initialColor.a = 255;
        initialColor.r = 46;
        initialColor.g = 126;
        initialColor.b = 2;
    }

    public void ResetBarColor()
    {
        lifeBarImage.color = initialColor;
    }

    public void SetBarColor(Color color)
    {
        lifeBarImage.color = color;
    }

    private void Update()
    {
        LifeBarAnimation();
    }

    private void LifeBarAnimation()
    {
        if(currentHealth != uiHealth)
        {
            currentLerpTime += Time.deltaTime;
            uiHealth = Mathf.Lerp(uiHealth, currentHealth, currentLerpTime);
            lifeBarImage.fillAmount = uiHealth / MaxHealth;
        }
    }

    public void UpdateLifeBar(int currentHealthvalue)
    {
        currentHealth = currentHealthvalue;
        currentLerpTime = 0f;

        if (percentage != null)
            percentage.text = (currentHealthvalue * 100 / MaxHealth).ToString();
    }

    public void SetBarVisible(bool boolean)
    {
        IsVisible = boolean;
        lifeBar.SetActive(IsVisible);
    }

    public void SetHealthController(HealthController health)
    {
        health.OnUpdateLife += UpdateLifeBar;
        MaxHealth = health.MaxHealth;
        uiHealth = MaxHealth;
        currentHealth = MaxHealth;
    }
}
