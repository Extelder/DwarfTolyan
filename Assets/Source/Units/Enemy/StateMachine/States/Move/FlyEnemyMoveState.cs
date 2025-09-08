using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class FlyEnemyMoveState : EnemyMoveState
{
    [SerializeField] private EnemyAttackStateMachine _stateMachine;

    [SerializeField] private float _speed;

    [SerializeField] private float _stopDistance;
    
    [SerializeField] private Rigidbody _rigidbody;

    private CompositeDisposable _moveDisposable = new CompositeDisposable();

    public override void Enter()
    {
        _stateMachine.Attack();
        Observable.EveryUpdate().Subscribe(_ =>
        {
            Vector3 _newPosition = transform.position;
            _newPosition.y += Mathf.Sin(Time.time) * Time.deltaTime;
            transform.position = _newPosition;

            if (Vector3.Distance(transform.position, Character.PlayerTransform.position) > _stopDistance)
                _rigidbody.velocity = transform.forward.normalized * _speed;
            else
                _rigidbody.velocity = new Vector3(0, 0, 0);
        }).AddTo(_moveDisposable);
    }

    public void StopMove()
    {
        _moveDisposable?.Clear();
    }

    private void OnDisable()
    {
        _moveDisposable.Clear();
    }
}
