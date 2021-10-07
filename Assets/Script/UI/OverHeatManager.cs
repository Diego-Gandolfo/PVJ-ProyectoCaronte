using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OverHeatManager : MonoBehaviour
{
    [SerializeField] private GameObject container;
    [SerializeField] private Image fillImage;
    [SerializeField] private Text percentage;

    public bool IsVisible { get; private set; }

    public void UpdateStatBar(float currentStat, float maxStat)
    {
        if (fillImage != null)
            fillImage.fillAmount = currentStat / maxStat;

        if (percentage != null)
            percentage.text = (currentStat * 100 / maxStat).ToString();
    }

    public void SetBarVisible(bool boolean)
    {
        IsVisible = boolean;
        container.SetActive(IsVisible);
    }
}
