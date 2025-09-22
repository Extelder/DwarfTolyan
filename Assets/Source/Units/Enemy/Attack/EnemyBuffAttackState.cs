using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBuffAttackState : State
{
    [SerializeField] private EnemyStateMachine _stateMachine;
    [SerializeField] private PlayerEnemyOverlapCheck _check;
    [SerializeField] private Animator _animator;
    [SerializeField] private string _animatorBoolName;
    [SerializeField] private RaycastSettings _raycastSettings;
    [SerializeField] private float _addibleCharacteristicValue;
    [SerializeField] private int _maxClosestEnemyCapacity;
    
    [SerializeField] private List<EnemyHitBox> _enemyHitBoxes = new List<EnemyHitBox>();
    [SerializeField] private List<EnemyHitBox> _closestEnemyHitBoxes = new List<EnemyHitBox>();

    public override void Enter()
    {
        _animator.SetBool(_animatorBoolName, true);
        _stateMachine.CurrentState.CanChanged = false;
    }
    
    private void OnEnable()
    {
        _closestEnemyHitBoxes = new List<EnemyHitBox>(_maxClosestEnemyCapacity);
        _check.EnemyDetected += OnEnemyDetected;
        _check.EnemyLost += OnEnemyLost;
    }

    private void OnEnemyLost()
    {
        _enemyHitBoxes.Clear();
        _closestEnemyHitBoxes.Clear();
        _stateMachine.CurrentState.CanChanged = true;
        _animator.SetBool(_animatorBoolName, false);
    }

    private void OnEnemyDetected(EnemyHitBox enemyHitBox)
    {
        if (_enemyHitBoxes.Contains(enemyHitBox))
            return;
        _enemyHitBoxes.Add(enemyHitBox);
        CheckDistance();
    }

    public void PerformAttack()
    {
        foreach (var other in _closestEnemyHitBoxes)
        {
            if (Physics.Raycast(_raycastSettings.RayOrigin.position, other.transform.position - _raycastSettings.RayOrigin.position, out RaycastHit hit,
                _raycastSettings.MaxDistance, _raycastSettings.LayerMask))
            {
                if (hit.collider.TryGetComponent<IBuffable>(out IBuffable buffable))
                {
                    Debug.Log("Buffed");
                    buffable.Buff(_addibleCharacteristicValue);
                }
            }
        }
    }
    
    private void CheckDistance()
    {
        float minDistance = Single.PositiveInfinity;
        foreach (var other in _enemyHitBoxes)
        {
            if (other.Distance < minDistance)
            {
                _closestEnemyHitBoxes.Add(other);
                minDistance = other.Distance;
                if (_closestEnemyHitBoxes.Count >= _closestEnemyHitBoxes.Capacity)
                {
                    _closestEnemyHitBoxes.RemoveAt(0);
                    Debug.Log(_enemyHitBoxes + "gavno");
                }
            }
        }
    }

    private void OnDisable()
    {
        _check.EnemyDetected -= OnEnemyDetected;
        _check.EnemyLost -= OnEnemyLost;
    }
}
