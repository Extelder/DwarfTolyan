using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MegaLaserGunShoot : LaserGunShoot
{
    private LaserGunShoot _laserGunShoot;

    public MegaLaserGunShoot(LaserGunShoot laserGunShoot)
    {
        _laserGunShoot = laserGunShoot;
        _laserGunShoot._instance = true;
    }

    public override void Bootstrap(Transform shootOrigin, float range, Turret turret, LayerMask layerMask)
    {
        base.Bootstrap(shootOrigin, range, turret, layerMask);

        _laserGunShoot.Bootstrap(shootOrigin, range, turret, _layerMask);
    }

    public override void Shoot()
    {
        Debug.LogError("LaserLaser");
        _laserGunShoot.Shoot();
    }
}