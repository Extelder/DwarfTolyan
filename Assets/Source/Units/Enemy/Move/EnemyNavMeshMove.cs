using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavMeshMove : EnemyMove
{
    [SerializeField] private float _updatePositionRate;

    [SerializeField] private NavMeshAgent _agent;

    private CompositeDisposable _disposable = new CompositeDisposable();

    private Transform _playerPosition;

    protected override void AwakeVirtual()
    {
        _playerPosition = Character.PlayerTransform;
        StartMove();
    }

    public void StartMove()
    {
        Observable.Interval(TimeSpan.FromSeconds(_updatePositionRate)).Subscribe(_ =>
            {
                _agent.SetDestination(_playerPosition.position);
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