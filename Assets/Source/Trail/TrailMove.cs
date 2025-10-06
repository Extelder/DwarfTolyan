using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class TrailMove : MonoBehaviour
{
    [SerializeField] private float _speed;
    
    private CompositeDisposable _disposable = new CompositeDisposable();

    private void OnEnable()
    {
        Move();
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
