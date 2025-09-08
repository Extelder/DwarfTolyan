using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavmeshMoveState : EnemyMoveState
{
    [SerializeField] private float _updatePositionRate;

    [SerializeField] private NavMeshAgent _agent;

    private CompositeDisposable _disposable = new CompositeDisposable();

    protected Transform targetPosition;

    public override void Enter()
    {
        StartMove();
    }

    protected override void AwakeVirtual()
    {
        targetPosition = Character.PlayerTransform;
    }

    public void StartMove()
    {
        Observable.Interval(TimeSpan.FromSeconds(_updatePositionRate)).Subscribe(_ =>
            {
                _agent.SetDestination(targetPosition.position);
            })
            .AddTo(_disposable);
    }

    public void StopMove()
    {
        _disposable?.Clear();
    }

    private void OnDisable()
    {
        StopMove();
    }
}
