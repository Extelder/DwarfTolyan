using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public abstract class BaseTurretShootAbility
{
    public virtual void DrawGizmos()
    {
    }
    
    [field: SerializeField] public abstract WeaponShootData ShootData { get; protected set; }
    public abstract void StartShooting(ref CompositeDisposable disposable, Action Shoot);
    public abstract void StopShooting(ref CompositeDisposable disposable);

    
    public abstract void Shoot();
}
