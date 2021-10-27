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
