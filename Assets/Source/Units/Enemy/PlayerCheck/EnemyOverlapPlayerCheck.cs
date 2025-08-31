using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

[Serializable]
public struct OverlappSettings
{
    [field: SerializeField] public LayerMask CheckLayerMask { get; private set; }
    [field: SerializeField] public float CheckRange { get; private set; }
    [field: SerializeField] public Transform CheckPoint { get; private set; }
}

public class EnemyOverlapPlayerCheck : EnemyPlayerCheck
{
    [SerializeField] private OverlappSettings _overlappSettings;
    [SerializeField] private float _checkRate;
    [SerializeField] private int _colliderCount;

    private Collider[] _others;

    private CompositeDisposable _disposable = new CompositeDisposable();

    public override event Action<PlayerHitBox> PlayerDetected;
    public override event Action PlayerLost;

    private void Start()
    {
        StartChecking();
    }

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

                if (_others[i].TryGetComponent<ShieldHitBox>(out ShieldHitBox shieldHitBox))
                {
                    PlayerDetected?.Invoke(shieldHitBox);
                    return;
                }
                
                if (_others[i].TryGetComponent<PlayerHitBox>(out PlayerHitBox playerHitBox))
                {
                    PlayerDetected?.Invoke(playerHitBox);
                    return;
                }
            }

            PlayerLost?.Invoke();
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