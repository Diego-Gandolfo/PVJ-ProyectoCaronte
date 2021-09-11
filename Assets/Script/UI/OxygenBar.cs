using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OxygenBar : MonoBehaviour
{
    [SerializeField] private GameObject oxygenBar;
    [SerializeField] private Image oxygenBarImage;
    private bool isVisible;

    private void Start()
    {
        GameManager.instance.Player.GetComponent<OxygenSystemController>().OnChangeInOxygen += UpdateOxygenBar;
    }

    public void UpdateOxygenBar(float currentOxygen, float maxOxygen)
    {
        var percentage = currentOxygen / maxOxygen;
        if (oxygenBarImage != null)
            if(percentage != 100)
            {
                SetBarVisible(true);
                oxygenBarImage.fillAmount = percentage;
            } else
            {
                SetBarVisible(false);
            }
    }

    public void SetBarVisible(bool value)
    {
        isVisible = value;
        oxygenBar.SetActive(isVisible);
    }
}
