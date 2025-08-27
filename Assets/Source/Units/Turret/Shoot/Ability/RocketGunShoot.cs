using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketGunShoot : TurretShootAbility
{
    [SerializeField] private Pool _projectilePool;
    [SerializeField] protected Transform _shootOrigin;
    [SerializeField] protected float _range;

    public override void Shoot()
    {
        Debug.Log("Machine gun shoot ");
        Vector3 direction = _shootOrigin.position + _shootOrigin.forward * _range;
        Projectile projectile = _projectilePool
            .GetFreeElement(_shootOrigin.position, Quaternion.FromToRotation(_shootOrigin.position, direction))
            .GetComponent<Projectile>();
        projectile.Initiate(direction);
    }
}
