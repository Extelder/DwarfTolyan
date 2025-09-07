using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnimator : UnitAnimator
{
    [SerializeField] private string _shootAnimationTriggerName;

    private void OnValueChanged(float value)
    {
        Animator.speed = value;
    }

    private void OnDisable()
    {
        AttackSpeedCharacteristic.Instance.ValueChanged -= OnValueChanged;
    }

    private void Start()
    {
        AttackSpeedCharacteristic.Instance.ValueChanged += OnValueChanged;
        Idle();
    }

    public override void DisableAllBools()
    {
        SetAnimationBool(_shootAnimationTriggerName, false);
    }

    public void Idle()
    {
        DisableAllBools();
    }

    public void Shoot()
    {
        SetAnimationTrigger(_shootAnimationTriggerName);
    }
}