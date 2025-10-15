using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMixBuffDamage : MonoBehaviour
{
    [SerializeField] private WeaponMix _weaponMix;
    
    private float _defaultDamage;
    
    private float _kayotTime;
    private float _addibleDamage;
    
    private DamageCharacterics _damageCharacterics;
    
    private GunMixAddibleDamageCharacteristics _gunMixAddibleDamage;
    private GunMixKayotTimeCharacteristics _gunMixKayotTime;

    private void OnEnable()
    {
        _weaponMix.MixPerformed += OnMixPerformed;
    }

    private void Start()
    {
        _damageCharacterics = DamageCharacterics.Instance;
        _defaultDamage = _damageCharacterics.CurrentValue;
        
        _gunMixAddibleDamage = GunMixAddibleDamageCharacteristics.Instance;
        _gunMixKayotTime = GunMixKayotTimeCharacteristics.Instance;
        
        _gunMixKayotTime.ValueChanged += OnKayotValueChanged;
        _gunMixAddibleDamage.ValueChanged += OnAddibleDamageChanged;
    }

    private void OnAddibleDamageChanged(float value)
    {
        _addibleDamage = value;
    }

    private void OnMixPerformed()
    {
        StartCoroutine(BuffDamage());
    }

    private IEnumerator BuffDamage()
    {
        _damageCharacterics.AddValue(_addibleDamage);
        yield return new WaitForSeconds(_kayotTime);
        _damageCharacterics.CurrentValue = _defaultDamage;
        StopAllCoroutines();
    }

    private void OnKayotValueChanged(float value)
    {
        _kayotTime = value;
    }

    private void OnDisable()
    {
        _weaponMix.MixPerformed -= OnMixPerformed;
        _gunMixKayotTime.ValueChanged -= OnKayotValueChanged;
        _gunMixAddibleDamage.ValueChanged -= OnAddibleDamageChanged;
    }
}
