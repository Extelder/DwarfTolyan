using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MegaFlamethrowerShoot : FlamethrowerShoot
{
    private FlamethrowerShoot _machineGunShoot;

    public MegaFlamethrowerShoot(FlamethrowerShoot machineGunShoot)
    {
        _machineGunShoot = machineGunShoot;
        _machineGunShoot._instance = true;
    }

    public override void Bootstrap(Transform shootOrigin, float range, Turret turret, LayerMask layerMask, int count)
    {
        base.Bootstrap(shootOrigin, range, turret, layerMask, count);

        _machineGunShoot.Bootstrap(shootOrigin, range, turret, layerMask, count);
    }

    public override void Shoot()
    {
        Debug.LogError("FlameThrower");
        _machineGunShoot.Shoot();
    }
}