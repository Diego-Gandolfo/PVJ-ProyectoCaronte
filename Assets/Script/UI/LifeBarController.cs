using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeBarController : MonoBehaviour
{
    [SerializeField] private GameObject lifeBar;
    [SerializeField] private Image lifeBarImage;
    [SerializeField] private Text percentage;

    public void UpdateLifeBar(int currentHealth, int maxHealth)
    {
        if(lifeBarImage != null)
            lifeBarImage.fillAmount = (float) currentHealth / maxHealth;

        if(percentage != null)
            percentage.text = (currentHealth * 100 / maxHealth).ToString();
    }

    public void VisibleBar(bool boolean)
    {
        lifeBar.SetActive(boolean);
    }
}
