using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponMix : MonoBehaviour
{
    [SerializeField] private ProjectileWeaponShoot _weaponShoot;
    [SerializeField] private Animator _animator;
    [SerializeField] private Animator _hammerAnimator;
    [SerializeField] private Animator _nailgunAnimator;

    private PlayerCharacter _character;

    private void Start()
    {
        _character = PlayerCharacter.Instance;
        _character.Binds.Character.MainShoot.started += OnMainShootStarted;
        _character.Binds.Character.SecondaryShoot.started += OnMainShootStarted;
    }

    private void OnMainShootStarted(InputAction.CallbackContext obj)
    {
        if (_character.Binds.Character.MainShoot.WasPressedThisFrame() &&
            _character.Binds.Character.SecondaryShoot.WasPressedThisFrame())
        {
            _hammerAnimator.enabled = false;
            _nailgunAnimator.enabled = false;

            _animator.SetTrigger("Mix");
        }
    }

    public void AnimationEnd()
    {
        _hammerAnimator.enabled = true;
        _nailgunAnimator.enabled = true;
    }

    public void PerformMix()
    {
        _weaponShoot.ChangeShootType();
    }

    private void OnDisable()
    {
        _character.Binds.Character.MainShoot.started -= OnMainShootStarted;
        _character.Binds.Character.SecondaryShoot.started -= OnMainShootStarted;
    }
}