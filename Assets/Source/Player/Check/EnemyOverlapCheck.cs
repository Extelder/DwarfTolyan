using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class EnemyOverlapCheck : EnemyCheck
{
    [SerializeField] private OverlappSettings _overlappSettings;
    [SerializeField] private float _checkRate;
    [SerializeField] private int _colliderCount;
    [SerializeField] private EnemyAttackStateMachine _stateMachine;
    [SerializeField] private Collider _enemyCollider;
    
    private Collider[] _others;
    private CompositeDisposable _disposable = new CompositeDisposable();
    
    public override event Action EnemyDetected;
    public override event Action EnemyLost;
    
    public override void StartChecking()
    {
        Observable.Interval(TimeSpan.FromSeconds(_checkRate)).Subscribe(_ =>
        {
            _others = new Collider[_colliderCount];
            Physics.OverlapSphereNonAlloc(_overlappSettings.CheckPoint.position, _overlappSettings.CheckRange, _others,
                _overlappSettings.CheckLayerMask);

            for (int i = 0; i < _others.Length; i++)
            {
                if (_others[i] == null)
                {
                    continue;
                }
                
                if (_others[i] == _enemyCollider)
                {
                    continue;
                }
                
                if (_others[i].TryGetComponent<EnemyHitBox>(out EnemyHitBox enemyHitBox))
                {
                    EnemyDetected?.Invoke();
                    _stateMachine.Attack();
                    return;
                }
            }

            EnemyLost?.Invoke();
        }).AddTo(_disposable);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(_overlappSettings.CheckPoint.position, _overlappSettings.CheckRange);
    }

    public override void StopChecking()
    {
        _disposable?.Clear();
    }

    private void OnDisable()
    {
        StopChecking();
    }
}
