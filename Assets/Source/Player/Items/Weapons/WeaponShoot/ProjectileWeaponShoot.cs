using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using Random = UnityEngine.Random;

public enum ShootType
{
    Rifle,
    Shotgun
}

public class ProjectileWeaponShoot : WeaponShoot
{
    [SerializeField] private AudioSource _shotGunSound;
    [SerializeField] private float _percentToCrit;

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

    private DamageCharacterics _damageCharacterics;
    private float _defaultCrit;
    private CriticalDamageCharacteristics _critDamageCharacterics;
    private Pool _currentPool;

    private void OnValueChanged(float value)
    {
        _defaultCrit *= value;
    }

    private void Start()
    {
        Initiate();
        _damageCharacterics = DamageCharacterics.Instance;
        _critDamageCharacterics = CriticalDamageCharacteristics.Instance;

        _defaultCrit = _critDamageCharacterics.CurrentValue;

        _critDamageCharacterics.ValueChanged += OnValueChanged;
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

        float damage = _damageCharacterics.CurrentValue;
        if (Random.value <= _percentToCrit)
        {
            damage *= _defaultCrit;
        }

        CameraShakeInvoke();
        switch (CurrentShootType)
        {
            case ShootType.Rifle:

                BulletSpawned?.Invoke();
                Vector3 directionRifle = Camera.position + Camera.forward * Range;
                if (GetHitCollider(out Collider collider))
                {
                    directionRifle = GetRaycastHit().point;
                }

                Projectile projectileRifle = _currentPool
                    .GetFreeElement(_muzzle.position, Quaternion.FromToRotation(_muzzle.position, directionRifle))
                    .GetComponent<Projectile>();
                projectileRifle.Initiate(directionRifle, damage, true);
                break;

            case ShootType.Shotgun:
                _shotGunSound.Play();

                for (int i = 0; i < _shotgunCharges; i++)
                {
                    BulletSpawned?.Invoke();
                    Vector3 random = Random.insideUnitSphere * _shotgunRandomMultipier;

                    Vector3 direction = Camera.position + random + Camera.forward * Range;
                    if (GetHitCollider(out Collider collider2))
                    {
                        direction = GetRaycastHit().point + random;
                    }


                    Projectile projectileShotGun = _currentPool
                        .GetFreeElement(_muzzle.position + random,
                            Quaternion.FromToRotation(_muzzle.position, direction))
                        .GetComponent<Projectile>();
                    projectileShotGun.Initiate(direction, damage, true);
                }

                break;
        }
    }

    public override void OnDisableVirtual()
    {
        _critDamageCharacterics.ValueChanged -= OnValueChanged;
    }
}