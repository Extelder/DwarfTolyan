using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class WeaponOverheat : MonoBehaviour
{
    [SerializeField] private float _maxValue;
    [SerializeField] private float _decreaseValue;
    [SerializeField] private float _decreaseRate;
    [SerializeField] private float _exitOverheatedTime;

    [SerializeField] private float _increaseValue;

    [SerializeField] private DefaultWeaponShootState _weaponShootState;
    [SerializeField] private WeaponShoot _weaponShoot;

    public float CurrentValue { get; private set; }

    private CompositeDisposable _disposable = new CompositeDisposable();

    private bool _decresing;

    public event Action<float> CurrentValueChanged;

    public event Action Overheated;
    public event Action Normilized;

    private void Start()
    {
        CurrentValue = 0;
    }

    private void OnEnable()
    {
        _weaponShoot.ShootPerformed += OnShootPerformed;
    }

    private IEnumerator StartDecreasing()
    {
        while (true)
        {
            yield return new WaitForSeconds(_decreaseRate);

            if (CurrentValue >= _maxValue)
            {
                yield return new WaitForSeconds(_exitOverheatedTime);
            }

            CurrentValue -= _decreaseValue;
            if (CurrentValue < 0)
                CurrentValue = 0;

            CurrentValueChanged?.Invoke(CurrentValue);
            if (CurrentValue < _maxValue - 0.1f)
            {
                NormalizeWeapon();
            }
        }
    }

    public void OverheatWeapon()
    {
        if (!_weaponShootState.CanShoot)
            return;

        Overheated?.Invoke();
        _weaponShootState.CanShoot = false;
    }

    public void NormalizeWeapon()
    {
        if (_weaponShootState.CanShoot)
            return;

        Normilized?.Invoke();
        _weaponShootState.CanShoot = true;
    }

    private void OnShootPerformed()
    {
        StopAllCoroutines();
        StartCoroutine(StartDecreasing());
        CurrentValue += _increaseValue;
        if (CurrentValue >= _maxValue)
        {
            OverheatWeapon();
            CurrentValue = _maxValue;
        }

        CurrentValueChanged?.Invoke(CurrentValue);
    }

    private void OnDisable()
    {
        _disposable?.Clear();
        _weaponShoot.ShootPerformed -= OnShootPerformed;
    }
}