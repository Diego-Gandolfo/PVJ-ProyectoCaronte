using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIQuestBoxAnimation : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private void Start()
    {
        //_animator = GetComponent<Animator>();
    }

    public void QuestBoxVisible()
    {
        _animator.SetBool("ShowOn", true);
    }

    public void QuestBoxInvisible()
    {
        _animator.SetBool("ShowOn", false);
    }

    public void StartTransition()
    {
        _animator.SetBool("HasFinishedTransition", false);
    }

    public void HasFinishedTransition()
    {
        _animator.SetBool("HasFinishedTransition", true);
    }
}
