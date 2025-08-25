using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class TurretShootBuff : MonoBehaviour
{
    [field: SerializeReference] [field: SerializeReferenceButton] [SerializeField]
    private TurretShootAbilityModificator _turretShootAbility;

    [SerializeField] private Turret _turret;

    [Button()]
    public void Pickup()
    {
        _turret.ModifyShoot(_turretShootAbility);
    }
}