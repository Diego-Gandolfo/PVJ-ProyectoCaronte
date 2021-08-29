using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeBarController : MonoBehaviour
{
    [SerializeField] private Image lifeBar;
    [SerializeField] private Text percentage;

    public void UpdateLifeBar(int currentHealth, int maxHealth)
    {
        lifeBar.fillAmount = (float) currentHealth / maxHealth;

        percentage.text = (currentHealth * 100 / maxHealth).ToString();
    }
}
