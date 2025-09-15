using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class TrailMove : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private TrailRenderer _trail;
    [SerializeField] private float _defaultTime;
    
    private CompositeDisposable _disposable = new CompositeDisposable();

    private void OnEnable()
    {
        _trail.time = -1;
        Invoke(nameof(ResetTrail), 0.02f);
        Move();
    }

    private void ResetTrail()
    {
        _trail.time = _defaultTime;
    }

    private void Move()
    {
        Observable.EveryUpdate().Subscribe(_ =>
        {
            transform.position += transform.forward * _speed * Time.deltaTime;
        }).AddTo(_disposable);
    }

    private void OnDisable()
    {
        _disposable.Clear();
    }
}
