using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private string _attackAnimationBoolName;

    [SerializeField] private EnemyPlayerCheck _playerCheck;

    private void OnEnable()
    {
        _playerCheck.PlayerDetected += OnPlayerDetected;
        _playerCheck.PlayerLost += OnPlayerLost;
    }

    private void OnPlayerLost()
    {
        _animator.SetBool(_attackAnimationBoolName, false);
    }

    private void OnPlayerDetected()
    {
        _animator.SetBool(_attackAnimationBoolName, true);
    }

    private void OnDisable()
    {
        _playerCheck.PlayerDetected -= OnPlayerDetected;
        _playerCheck.PlayerLost -= OnPlayerLost;
    }
}