using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoolMachineGunShoot : TurretShootAbilityModificator
{
    public CoolMachineGunShoot(BaseTurretShootAbility turretShootAbility) : base(turretShootAbility)
    {
        
    }

    public override void Shoot()
    {
        base.Shoot();
        Debug.Log("Cool");
    }
}
