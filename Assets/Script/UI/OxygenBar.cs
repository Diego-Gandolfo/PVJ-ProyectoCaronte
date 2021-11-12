using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OxygenBar : MonoBehaviour
{
    #region Serialized Fields

    [SerializeField] private GameObject oxygenBar;
    [SerializeField] private GameObject vignette;
    [SerializeField] private Image oxygenBarImage;
    [SerializeField]private float lerpTime;

    #endregion

    #region Private Fields

    // Components
    private Animator vignetteAnimator;
    private Animator animator;

    // Parameters
    private bool isVisible;

    #endregion

    #region Unity Methods

    private void Awake()
    {
        LevelManager.instance.OnPlayerAssing += OnPlayerAssing;
        animator = GetComponent<Animator>();
        vignetteAnimator = vignette.GetComponent<Animator>();
    }

    private void Update()
    {
        lerpTime +=Time.deltaTime * Time.deltaTime;
    }

    #endregion

    #region Protected Methods

    protected void OnPlayerAssing(PlayerController player)
    {
        LevelManager.instance.OnPlayerAssing -= OnPlayerAssing;
        player.GetComponent<OxygenSystemController>().OnChangeInOxygen += UpdateOxygenBar;
    }

    #endregion

    #region Public Methods

    public void UpdateOxygenBar(float currentOxygen, float maxOxygen)
    {
        if (oxygenBarImage != null)
        {
            oxygenBarImage.fillAmount = Mathf.Lerp(oxygenBarImage.fillAmount, (currentOxygen/maxOxygen), lerpTime);
            animator.SetBool("Dying", currentOxygen <= (maxOxygen / 3));
            vignetteAnimator.SetBool("Dying", currentOxygen <= (maxOxygen / 4));
        }
    }

    public void SetBarVisible(bool value)
    {
        isVisible = value;
        oxygenBar.SetActive(isVisible);
    }

    #endregion
}
