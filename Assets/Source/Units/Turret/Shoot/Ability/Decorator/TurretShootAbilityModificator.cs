using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TurretShootAbilityModificator : TurretShootAbility
{
    protected BaseTurretShootAbility CurrentShootAbility;

    public TurretShootAbilityModificator(BaseTurretShootAbility turretShootAbility, WeaponShootData weaponShootData)
    {
        CurrentShootAbility = turretShootAbility;
        ShootData = weaponShootData;
    }
    
    public TurretShootAbilityModificator()
    {

    }

    public override void Shoot()
    {
        base.Shoot();
        CurrentShootAbility.Shoot();
    }
}