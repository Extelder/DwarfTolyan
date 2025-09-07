using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGunShoot : TurretShootAbility
{
    [SerializeField] public bool _instance;

    [SerializeField] protected Transform _shootOrigin;
    [SerializeField] protected float _range;
    [SerializeField] protected Turret _turret;

    public static MachineGunShoot Instance { get; private set; }

    public void SetInstance()
    {
        _instance = true;
        Instance = this;
    }

    public virtual void Bootstrap(Transform shootOrigin, float range, Turret turret)
    {
        _shootOrigin = shootOrigin;
        _range = range;
        _turret = turret;
    }

    public override void Shoot()
    {
        if (_instance == false)
        {
            Instance.Bootstrap(_shootOrigin, _range, _turret);
            Instance.Shoot();
            return;
        }

        Vector3 direction = _shootOrigin.position + _shootOrigin.forward * _range;
        Projectile projectile = Pools.Instance.TurretMachineGunProjectilePool.GetFreeElement
                (_shootOrigin.position, Quaternion.FromToRotation(_shootOrigin.position, direction))
            .GetComponent<Projectile>();
        projectile.Initiate(direction, _turret.Damage);
    }
}