using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIQuestBoxAnimation : MonoBehaviour
{
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void QuestBoxVisible()
    {
        _animator.SetBool("ShowOn", true);
    }

    public void QuestBoxInvisible()
    {
        _animator.SetBool("ShowOn", false);
    }

    public void TriggerShow()
    {
        _animator.SetTrigger("Show");
    }

    public void QuestBoxShow(bool value)
    {
        if(value)
        {
            _animator.SetTrigger("Show");
        }
        else
            _animator.SetTrigger("Hide");
    }
}
