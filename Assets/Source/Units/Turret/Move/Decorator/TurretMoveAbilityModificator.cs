using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TurretMoveAbilityModificator : TurretMoveAbility
{
    protected BaseTurretMove CurrentShootAbility;

    public TurretMoveAbilityModificator(BaseTurretMove turretShootAbility, TurretMoveData turretMoveData)
    {
        CurrentShootAbility = turretShootAbility;
        MoveData = turretMoveData;
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