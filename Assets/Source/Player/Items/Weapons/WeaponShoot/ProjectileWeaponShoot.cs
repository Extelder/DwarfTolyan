using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeaponShoot : WeaponShoot
{
    [SerializeField] private Transform _muzzle;


    private Pool _currentPool;

    private void Start()
    {
        Initiate();
    }

    public virtual void Initiate()
    {
        _currentPool = Pools.Instance.DefaultProjectilePool;
    }

    public override void OnShootPerformed()
    {
        base.OnShootPerformed();

        CameraShakeInvoke();
        Vector3 direction = Camera.position + Camera.forward * Range;
        Projectile projectile = _currentPool
            .GetFreeElement(_muzzle.position, Quaternion.FromToRotation(_muzzle.position, direction))
            .GetComponent<Projectile>();
        projectile.Initiate(direction);
    }
}