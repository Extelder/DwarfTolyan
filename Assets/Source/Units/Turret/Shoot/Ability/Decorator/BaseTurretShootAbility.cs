using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public abstract class BaseTurretShootAbility
{
    [field: SerializeField] public abstract WeaponShootData ShootData { get; protected set; }
    public abstract void StartShooting(ref CompositeDisposable disposable, Action Shoot);

    
    public abstract void Shoot();
}
