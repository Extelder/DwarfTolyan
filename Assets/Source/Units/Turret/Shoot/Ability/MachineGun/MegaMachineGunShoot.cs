using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MegaMachineGunShoot : MachineGunShoot
{
    private MachineGunShoot _machineGunShoot;

    public MegaMachineGunShoot(MachineGunShoot machineGunShoot)
    {
        _machineGunShoot = machineGunShoot;
        _machineGunShoot._instance = true;
    }

    public MegaMachineGunShoot()
    {
        
    }

    public override void Bootstrap(Transform shootOrigin, float range, Turret turret)
    {
        base.Bootstrap(shootOrigin, range, turret);

        _machineGunShoot.Bootstrap(shootOrigin, range, turret);
    }

    public override void Shoot()
    {
        Debug.LogError("DADADD");
        _machineGunShoot.Shoot();
    }
}