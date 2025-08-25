using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [field: SerializeReference]
    [field: SerializeReferenceButton]
    public BaseTurretShootAbility ShootAbility { get; set; }

    public BaseTurretMove TurretMove { get; set; }

    private WeaponShootData _shootData;
    private CompositeDisposable _shootingDisposable = new CompositeDisposable();

    public void Start()
    {
        ShootAbility.StartShooting(ref _shootingDisposable, () => {ShootAbility.Shoot();});
    }

    private void OnDisable()
    {
        _shootingDisposable?.Clear();
        StopAllCoroutines();
    }

    public void ModifyShoot(TurretShootAbilityModificator modificator)
    {
        var type = modificator.GetType();

        var newInstance = Activator.CreateInstance(type, ShootAbility, _shootData);
        ShootAbility = (BaseTurretShootAbility) newInstance;
    }

    public void ModifyMove(TurretMoveAbilityModificator modificator)
    {
        var type = modificator.GetType();

        var newInstance = Activator.CreateInstance(type, ShootAbility);
        TurretMove = (BaseTurretMove) newInstance;
    }
}