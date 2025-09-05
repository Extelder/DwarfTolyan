using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketGunShoot : TurretShootAbility
{
    [SerializeField] protected Transform _shootOrigin;
    [SerializeField] protected float _range;
    [SerializeField] protected Turret _turret;

    public override void Shoot()
    {
        Vector3 direction = _shootOrigin.position + _shootOrigin.forward * _range;
        Projectile projectile = Pools.Instance.TurretRPGProjectilePool.GetFreeElement
            (_shootOrigin.position, Quaternion.FromToRotation(_shootOrigin.position, direction)).
            GetComponent<Projectile>();
        projectile.Initiate(direction, _turret.Damage);
    }
}
