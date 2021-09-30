using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SonarManager : MonoBehaviour
{
    [SerializeField] private GameObject container;
    [SerializeField] private float MinDistanceLevel1 = 25f;
    [SerializeField] private float MinDistanceLevel2 = 15f;
    [SerializeField] private float MinDistanceLevel3 = 9f;

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
        if (level > 0 && level <= MinDistanceLevel3)
            animator.SetTrigger("Level3");
        else if(level > MinDistanceLevel3 && level <= MinDistanceLevel2)
            animator.SetTrigger("Level2");
        else if(level > MinDistanceLevel2 && level <= MinDistanceLevel1)
            animator.SetTrigger("Level1");
    }

}
