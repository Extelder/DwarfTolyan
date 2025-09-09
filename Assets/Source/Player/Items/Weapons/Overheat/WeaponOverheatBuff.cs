using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponOverheatBuff : MonoBehaviour
{
    [SerializeField] private float _moveSpeedBuff;
    [SerializeField] private float _buffSpeed;
    [SerializeField] private float _damageBuff;

    [SerializeField] private WeaponAnimator _weaponAnimator;

    [SerializeField] private WeaponOverheat _overheat;

    private void OnEnable()
    {
        _overheat.Overheated += OnOverheated;
        _overheat.Normilized += OnNormilized;
    }

    private void OnNormilized()
    {
        _weaponAnimator.Animator.speed -= _buffSpeed;
        DamageCharacterics.Instance.RemoveValue(_damageBuff);
    }

    private void OnOverheated()
    {
        _weaponAnimator.Animator.speed += _buffSpeed;
        DamageCharacterics.Instance.AddValue(_damageBuff);
    }

    private void OnDisable()
    {
        _overheat.Overheated -= OnOverheated;
        _overheat.Normilized -= OnNormilized;
    }
}