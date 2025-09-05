using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class Wave : MonoBehaviour
{
    [field: SerializeField] public Vector3 DefaultSpawnRadius { get; private set; }

    [SerializeField] private int _defaultTime;
    [SerializeField] private int _timeAddible;

    private int _currentTime;

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
        Time.timeScale = 1;
        Current++;
        Started?.Invoke(Current);
        _currentTime = _currentTime + _timeAddible * Current;

        int timePassed = 1;

        Observable.Interval(TimeSpan.FromSeconds(1)).TakeWhile(time => time <= _currentTime)
            .Subscribe(time =>
            {
                timePassed++;
                long delta = (int) _currentTime - timePassed;

                TimerCounted?.Invoke(delta);

                if (timePassed >= _currentTime)
                {
                    StopWave();
                }
            }).AddTo(_disposable);
    }

    public void StopWave()
    {
        _disposable?.Clear();
        Ended?.Invoke(Current);
        Time.timeScale = 0;
    }

    private void OnDisable()
    {
        _disposable?.Clear();
    }
}