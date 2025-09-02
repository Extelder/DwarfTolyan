using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class PlayerDash : Dashing
{
    [Header("Settings")] [SerializeField] private float _dashCooldown;

    private CompositeDisposable _dashDisposable = new CompositeDisposable();

    private bool _canDash = true;

    private PlayerBinds _binds;

    public event Action Dashed;

    private void OnEnable()
    {
        _binds = PlayerCharacter.Instance.Binds;
    }

    public void Dash()
    {
        if (!_canDash)
            return;

        if (!cooldownRecovered)
            return;

        Dashed?.Invoke();

        Vector3 inputDirection = new Vector3(_binds.Character.Horizontal.ReadValue<float>(), 0, 
            _binds.Character.Vertical.ReadValue<float>());

        Vector3 direction = orientation.TransformDirection(inputDirection);

        if (inputDirection == Vector3.zero)
        {
           
        }
        inputDirection = orientation.forward;
        direction = inputDirection;

        Vector3 forceToApply = (direction * dashSpeed + orientation.up * dashUpwardForce);
        float cooldown = _dashCooldown;

        AddImpulse(forceToApply, cooldown, _dashDisposable);
    }

    private void OnDisable()
    {
        _dashDisposable.Clear();
    }
}
