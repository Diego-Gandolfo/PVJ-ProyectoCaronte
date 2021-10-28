using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ActorStats", menuName = "ScriptableObject/ActorStats", order = 0)]
public class ActorStats : ScriptableObject //FLYWEIGTH
{
    public int MaxLife => _maxLife;
    [SerializeField] private int _maxLife = 100;

    public float OriginalSpeed => _speed;
    [SerializeField] private float _speed = 5f;

    public float BuffedSpeed => _buffedSpeed;
    [SerializeField] private float _buffedSpeed = 10f;

    public float OriginalAnimatorSpeed => _animatorSpeed;
    [SerializeField] private float _animatorSpeed = 1f;

    public float BuffedAnimatorSpeed => _buffedAnimatorSpeed;
    [SerializeField] private float _buffedAnimatorSpeed = 2f;

    public bool CanJump => canJump;
    [SerializeField] private bool canJump;

    public float JumpForce => jumpForce;
    [SerializeField] private float jumpForce;
}

