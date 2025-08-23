using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Turrent/Shoot")]
public abstract class TurretShootAbilityModificator : TurretShootAbility
{
    protected BaseTurretShootAbility CurrentShootAbility;

    public TurretShootAbilityModificator(BaseTurretShootAbility turretShootAbility)
    {
        CurrentShootAbility = turretShootAbility;
    }
    
    public virtual void Shoot()
    {
        CurrentShootAbility.Shoot();
    }
}