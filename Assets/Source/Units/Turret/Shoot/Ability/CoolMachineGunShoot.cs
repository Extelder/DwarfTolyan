using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoolMachineGunShoot : TurretShootAbilityModificator
{
    public CoolMachineGunShoot(BaseTurretShootAbility turretShootAbility, WeaponShootData weaponShootData) : base(turretShootAbility, weaponShootData)
    {
        
    }
    public CoolMachineGunShoot() 
    {
        
    }

    public override void Shoot()
    {
        base.Shoot();
        Debug.Log("Cool");
    }
}
