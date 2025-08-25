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

    [field: SerializeReference]
    [field: SerializeReferenceButton]
    public BaseTurretMove TurretMove { get; set; }

    private WeaponShootData _shootData;
    private TurretMoveData _turretMoveData;
    private CompositeDisposable _shootingDisposable = new CompositeDisposable();
    private CompositeDisposable _movingDisposable = new CompositeDisposable();

    public void Start()
    {
        ShootAbility.StartShooting(ref _shootingDisposable, () => { ShootAbility.Shoot(); });
        TurretMove.StartMoving(ref _movingDisposable, () => { TurretMove.Move(); });
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

        var newInstance = Activator.CreateInstance(type, ShootAbility, _turretMoveData);
        TurretMove = (BaseTurretMove) newInstance;
    }
}