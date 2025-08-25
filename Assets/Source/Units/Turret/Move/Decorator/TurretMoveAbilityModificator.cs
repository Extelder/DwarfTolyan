using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TurretMoveAbilityModificator : TurretMoveAbility
{
    protected BaseTurretMove CurrentShootAbility;

    public TurretMoveAbilityModificator(BaseTurretMove turretShootAbility)
    {
        CurrentShootAbility = turretShootAbility;
    }
    
    public TurretMoveAbilityModificator()
    {
        
    }

    public override void Move()
    {
        base.Move();
        CurrentShootAbility.Move();
    }
}
