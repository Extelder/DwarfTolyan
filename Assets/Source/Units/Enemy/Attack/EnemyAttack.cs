using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAttack : MonoBehaviour
{
    [field: SerializeField] public EnemyDamage Damage { get; private set; }


    [SerializeField] private Animator _animator;
    [SerializeField] private string _attackAnimationBoolName;

    [SerializeField] private EnemyPlayerCheck _playerCheck;

    private PlayerHitBox _hitBox;

    private void OnEnable()
    {
        _playerCheck.PlayerDetected += OnPlayerDetected;
        _playerCheck.PlayerLost += OnPlayerLost;
    }

    private void OnPlayerLost()
    {
        _animator.SetBool(_attackAnimationBoolName, false);
    }

    private void OnPlayerDetected(PlayerHitBox playerHitBox)
    {
        _animator.SetBool(_attackAnimationBoolName, true);
        _hitBox = playerHitBox;
    }

    private void PerformAttack()
    {
        _hitBox.TakeDamage(Damage.GetDamage());
    }

    private void OnDisable()
    {
        _playerCheck.PlayerDetected -= OnPlayerDetected;
        _playerCheck.PlayerLost -= OnPlayerLost;
    }
}