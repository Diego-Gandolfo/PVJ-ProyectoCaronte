using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICrystalCounter : MonoBehaviour
{
    [SerializeField] private Text text;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        LevelManager.instance.OnCrystalsInPlayerUpdate += UpdateCrystalCounter;
    }

    public void UpdateCrystalCounter(int number)
    {
        text.text = number.ToString();
    }

    public void ErrorAnimation()
    {
        animator.SetTrigger("Error");
    }

    public void OnDestroy()
    {
        LevelManager.instance.OnCrystalsInPlayerUpdate -= UpdateCrystalCounter;
    }
}
