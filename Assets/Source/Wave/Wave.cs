using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class Wave : MonoBehaviour
{
    [field: SerializeField] public Vector3 DefaultSpawnRadius { get; private set; }

    [SerializeField] private float _defaultTime;
    [SerializeField] private float _timeAddible;

    private float _currentTime;

    public int Current { get; private set; }

    public event Action<int> Started;
    public event Action<int> Ended;
    public event Action<long> TimerCounted;

    private CompositeDisposable _disposable = new CompositeDisposable();

    private void Start()
    {
        _currentTime = _defaultTime;
        StartWave();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(new Vector3(-DefaultSpawnRadius.x, DefaultSpawnRadius.y, -DefaultSpawnRadius.z),
            new Vector3(DefaultSpawnRadius.x, DefaultSpawnRadius.y, DefaultSpawnRadius.z));

        Gizmos.DrawLine(new Vector3(DefaultSpawnRadius.x, DefaultSpawnRadius.y, DefaultSpawnRadius.z),
            new Vector3(-DefaultSpawnRadius.x * -1, DefaultSpawnRadius.y, -DefaultSpawnRadius.z * -1));
    }

    public void StartWave()
    {
        Started?.Invoke(Current);
        _currentTime = _currentTime + _timeAddible * Current;
        Observable.Timer(TimeSpan.FromSeconds(_currentTime)).Subscribe(_ => { TimerCounted?.Invoke(_); })
            .AddTo(_disposable);
    }

    public void StopWave()
    {
        _disposable?.Clear();
        Ended?.Invoke(Current);
    }

    private void OnDisable()
    {
        _disposable?.Clear();
    }
}