using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackStats", menuName = "ScriptableObject/AttackStats", order = 1)]
public class AttackStats : ScriptableObject
{
    public LayerMask TargetList => _targetList;
    [SerializeField] private LayerMask _targetList;

    public int Damage => _damage;
    [SerializeField] private int _damage = 2;

    public float Cooldown => _cooldown;
    [SerializeField] private float _cooldown = 1f;

    public float BulletSpeed => _bulletSpeed;
    [SerializeField] private float _bulletSpeed = 7f;

    public float BulletLifeTimer => _bulletLifeTimer;
    [SerializeField] private float _bulletLifeTimer = 5f;
}
