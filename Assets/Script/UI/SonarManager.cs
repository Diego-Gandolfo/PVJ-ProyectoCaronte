using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SonarManager : MonoBehaviour
{
    [SerializeField] private GameObject container;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void SetVisibleSonar(bool value)
    {
        container.SetActive(value);
    }

    public void TriggerLevel(float level)
    {
        if (level > 0 && level <= 9) //entre 0 y 9
            animator.SetTrigger("Level3");
        else if(level > 9 && level <= 15) // entre 9 y 15
            animator.SetTrigger("Level2");
        else if(level > 15 && level <= 25)
            animator.SetTrigger("Level1"); //entre 15 y 25
    }

}
