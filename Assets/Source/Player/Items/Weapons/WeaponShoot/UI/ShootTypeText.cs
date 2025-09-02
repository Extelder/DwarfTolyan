using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShootTypeText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

    [SerializeField] private ProjectileWeaponShoot _projectileWeaponShoot;

    private void OnEnable()
    {
        _projectileWeaponShoot.CurrentShootTypeChanged += OnCurrentShootTypeChanged;
    }

    private void OnCurrentShootTypeChanged(ShootType shootType)
    {
        _text.text = shootType.ToString();
    }

    private void OnDisable()
    {
        _projectileWeaponShoot.CurrentShootTypeChanged -= OnCurrentShootTypeChanged;
    }
}