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
        LevelManager.instance.OnPlayerAssing += OnPlayerAssing;
        vignetteImageAnimator = vignetteImage.GetComponent<Animator>();
        oxygenBarAnimator = oxygenBar.GetComponentInChildren<Animator>();
        SetBarVisible(true);
    }

    public void UpdateOxygenBar(float currentOxygen, float maxOxygen)
    {
        var percentage = currentOxygen / maxOxygen;
        if (oxygenBarImage != null)
        {
            oxygenBarImage.fillAmount = percentage;
            oxygenBarAnimator.SetBool("IsRunningOut", currentOxygen <= 20);
            vignetteImageAnimator.SetBool("IsRunningOut", currentOxygen <= 20);
        }
    }

    public void SetBarVisible(bool value)
    {
        isVisible = value;
        oxygenBar.SetActive(isVisible);
    }

    protected void OnPlayerAssing(PlayerController player)
    {
        LevelManager.instance.OnPlayerAssing -= OnPlayerAssing;
        player.GetComponent<OxygenSystemController>().OnChangeInOxygen += UpdateOxygenBar;
    }
}
