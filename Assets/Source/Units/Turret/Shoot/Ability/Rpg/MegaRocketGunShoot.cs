using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MegaRocketGunShoot : RocketGunShoot
{
    private RocketGunShoot _machineGunShoot;

    public MegaRocketGunShoot(RocketGunShoot machineGunShoot)
    {
        _machineGunShoot = machineGunShoot;
        _machineGunShoot._instance = true;
    }

    public MegaRocketGunShoot()
    {
    }

    public override void Bootstrap(Transform shootOrigin, float range, Turret turret)
    {
        base.Bootstrap(shootOrigin, range, turret);

        _machineGunShoot.Bootstrap(shootOrigin, range, turret);
    }

    public override void Shoot()
    {
        Debug.LogError("Rocket Rocket");
        _machineGunShoot.Shoot();
    }
}