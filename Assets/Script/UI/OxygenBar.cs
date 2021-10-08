using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OxygenBar : MonoBehaviour
{
    [SerializeField] private GameObject oxygenBar;
    [SerializeField] private GameObject vignette;
    [SerializeField] private Image oxygenBarImage;

    private Animator vignetteAnimator;
    private Animator animator;

    private bool isVisible;

    private void Start()
    {
        LevelManager.instance.OnPlayerAssing += OnPlayerAssing;
        animator = GetComponent<Animator>();
        vignetteAnimator = vignette.GetComponent<Animator>();
    }

    public void UpdateOxygenBar(float currentOxygen, float maxOxygen)
    {
        var percentage = currentOxygen / maxOxygen;
        if (oxygenBarImage != null)
        {
            oxygenBarImage.fillAmount = percentage;
            animator.SetBool("Dying", currentOxygen <= 40);
            vignetteAnimator.SetBool("Dying", currentOxygen <= 20);
            //animator.SetBool("Dying", currentOxygen <= 99);
            //vignetteAnimator.SetBool("Dying", currentOxygen <= 99);
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
