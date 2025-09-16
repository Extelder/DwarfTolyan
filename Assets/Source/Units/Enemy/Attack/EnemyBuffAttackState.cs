using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBuffAttackState : State
{
    [SerializeField] private EnemyOverlapCheck _check;
    [SerializeField] private Animator _animator;
    [SerializeField] private string _animatorBoolName;
    [SerializeField] private RaycastSettings _raycastSettings;
    [SerializeField] private float _addibleCharacteristicValue;


    public override void Enter()
    {
        _animator.SetBool(_animatorBoolName, true);
    }
    
    private void OnEnable()
    {
        _check.EnemyDetected += OnEnemyDetected;
        _check.EnemyLost += OnEnemyLost;
    }

    private void OnEnemyLost()
    {
        _animator.SetBool(_animatorBoolName, false);
    }

    private void OnEnemyDetected()
    {
    }

    public void PerformAttack()
    {
        bool originRaycast = Physics.Raycast(_raycastSettings.RayOrigin.position, transform.forward, out RaycastHit hit,
            _raycastSettings.MaxDistance, _raycastSettings.LayerMask);
        if (originRaycast)
        {
            if (hit.collider.TryGetComponent<IBuffable>(out IBuffable buffable))
            {
                buffable.Buff(_addibleCharacteristicValue);
            }
        }
    }

    private void OnDisable()
    {
        _check.EnemyDetected -= OnEnemyDetected;
        _check.EnemyLost -= OnEnemyLost;
    }
}
