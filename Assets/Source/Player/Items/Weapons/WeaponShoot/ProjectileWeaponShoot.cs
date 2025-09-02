using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public enum ShootType
{
    Rifle,
    Shotgun
}

public class ProjectileWeaponShoot : WeaponShoot
{
    [SerializeField] private WeaponStateMachine _weaponStateMachine;
    [SerializeField] private DefaultWeaponShootState _defaultWeaponShootState;

    [SerializeField] private Animator _animator;
    [SerializeField] private RuntimeAnimatorController _shotGunAnimator;
    [SerializeField] private RuntimeAnimatorController _rifleAnimator;

    [SerializeField] private int _shotgunCharges;
    [SerializeField] private float _shotgunRandomMultipier;

    [SerializeField] private Transform _muzzle;

    public ShootType CurrentShootType;

    public event Action<ShootType> CurrentShootTypeChanged;

    private Pool _currentPool;

    private void Start()
    {
        Initiate();
    }

    public virtual void Initiate()
    {
        _animator.runtimeAnimatorController = _rifleAnimator;
        _currentPool = Pools.Instance.DefaultProjectilePool;
    }

    public void ResetWeapon()
    {
        _weaponStateMachine.StopAllCoroutines();
        _defaultWeaponShootState.StopAllCoroutines();
    }

    public void ChangeShootType()
    {
        switch (CurrentShootType)
        {
            case ShootType.Rifle:
                CurrentShootType = ShootType.Shotgun;
                _weaponStateMachine.CurrentState.CanChanged = true;
                _weaponStateMachine.Idle();
                _animator.runtimeAnimatorController = _shotGunAnimator;
                _weaponStateMachine.CurrentState.CanChanged = true;
                ResetWeapon();
                break;
            case ShootType.Shotgun:
                CurrentShootType = ShootType.Rifle;
                _weaponStateMachine.Idle();
                _animator.runtimeAnimatorController = _rifleAnimator;
                _weaponStateMachine.CurrentState.CanChanged = true;
                ResetWeapon();
                break;
        }

        CurrentShootTypeChanged?.Invoke(CurrentShootType);
    }

    public override void OnShootPerformed()
    {
        base.OnShootPerformed();

        CameraShakeInvoke();
        switch (CurrentShootType)
        {
            case ShootType.Rifle:
                Vector3 directionRifle = Camera.position + Camera.forward * Range;
                Projectile projectileRifle = _currentPool
                    .GetFreeElement(_muzzle.position, Quaternion.FromToRotation(_muzzle.position, directionRifle))
                    .GetComponent<Projectile>();
                projectileRifle.Initiate(directionRifle, DamageCharacterics.Instance.CurrentValue);
                break;

            case ShootType.Shotgun:
                for (int i = 0; i < _shotgunCharges; i++)
                {
                    Vector3 direction = Camera.position + Camera.forward * Range;
                    direction += Random.insideUnitSphere * _shotgunRandomMultipier;
                    Projectile projectile = _currentPool
                        .GetFreeElement(_muzzle.position, Quaternion.FromToRotation(_muzzle.position, direction))
                        .GetComponent<Projectile>();
                    projectile.Initiate(direction, DamageCharacterics.Instance.CurrentValue);
                }

                break;
        }
    }
}