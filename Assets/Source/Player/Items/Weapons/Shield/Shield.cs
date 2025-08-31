using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shield : MonoBehaviour
{
    [SerializeField] private GameObject _shieldParent;
    private PlayerBinds _binds;
 
    private void Start()
    {
        _binds = PlayerCharacter.Instance.Binds;
        _binds.Character.ShieldPickUp.started += OnShieldStarted;
        _binds.Character.ShieldPickUp.canceled += OnShieldCanceled;
    }

    private void OnShieldStarted(InputAction.CallbackContext obj)
    {
        _shieldParent.SetActive(true);
    }

    private void OnShieldCanceled(InputAction.CallbackContext obj)
    {
        _shieldParent.SetActive(false);
    }

    private void OnDisable()
    {
        _binds.Character.ShieldPickUp.started -= OnShieldStarted;
        _binds.Character.ShieldPickUp.canceled -= OnShieldCanceled;
    }
}
