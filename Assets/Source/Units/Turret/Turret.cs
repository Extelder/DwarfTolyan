using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private TurretShootAbilityModificator[] _shoots;

    [field: SerializeReference]
    [field: SerializeReferenceButton]
    public BaseTurretShootAbility ShootAbility { get; private set; }

    private IWeaponShootData _shootData;

    public void Start()
    {
        ShootAbility = new CoolMachineGunShoot(ShootAbility);
        ShootAbility.Shoot();
    }

    public void ModifyShootData(IWeaponShootData shootData)
    {
        _shootData = new WeaponShootData(shootData);
    }
}