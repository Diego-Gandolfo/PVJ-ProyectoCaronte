using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OxygenBar : MonoBehaviour
{
    [SerializeField] private GameObject oxygenBar;
    [SerializeField] private Image oxygenBarImage;
    [SerializeField] private RawImage vignetteImage;

    private Animator vignetteImageAnimator;
    private Animator oxygenBarAnimator;

    private bool isVisible;

    private void Start()
    {
        GameManager.instance.OnPlayerAssing += OnPlayerAssing;
        vignetteImageAnimator = vignetteImage.GetComponent<Animator>();
        oxygenBarAnimator = oxygenBar.GetComponentInChildren<Animator>();
    }

    public void UpdateOxygenBar(float currentOxygen, float maxOxygen)
    {
        var percentage = currentOxygen / maxOxygen;
        if (oxygenBarImage != null)
            if(percentage != 100)
            {
                SetBarVisible(true);
                oxygenBarImage.fillAmount = percentage;

                if (currentOxygen <= 20)
                {
                    oxygenBarAnimator.SetBool("IsRunningOut", true);
                    vignetteImageAnimator.SetBool("IsRunningOut", true);
                }
                else
                {
                    oxygenBarAnimator.SetBool("IsRunningOut", false);
                    vignetteImageAnimator.SetBool("IsRunningOut", false);
                }

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

    protected void OnPlayerAssing(PlayerController player)
    {
        GameManager.instance.OnPlayerAssing -= OnPlayerAssing;
        player.GetComponent<OxygenSystemController>().OnChangeInOxygen += UpdateOxygenBar;
    }
}
