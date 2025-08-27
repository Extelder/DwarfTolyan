using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGunShoot : TurretShootAbility
{
    [SerializeField] protected Transform _shootOrigin;
    [SerializeField] protected float _range;
    [SerializeField] protected Turret _turret;

    public override void Shoot()
    {
        Debug.Log("Machine gun shoot ");
        Vector3 direction = _shootOrigin.position + _shootOrigin.forward * _range;
        Projectile projectile = Pools.Instance.TurretMachineGunProjectilePool.GetFreeElement
                (_shootOrigin.position, Quaternion.FromToRotation(_shootOrigin.position, direction)).
            GetComponent<Projectile>();
        Debug.LogError(_turret.Damage);
        projectile.Initiate(direction, _turret.Damage);
    }
}