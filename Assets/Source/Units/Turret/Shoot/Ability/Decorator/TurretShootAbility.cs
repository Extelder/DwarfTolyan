using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class TurretShootAbility : BaseTurretShootAbility
{
    [field: SerializeField] public override WeaponShootData ShootData { get; protected set; }

    public override void StartShooting(ref CompositeDisposable disposable, Action shoot)
    {
        disposable = new CompositeDisposable();
        Observable.Interval(TimeSpan.FromSeconds(ShootData.GetRate())).Subscribe(_ => { shoot?.Invoke(); })
            .AddTo(disposable);
    }

    public override void StopShooting(ref CompositeDisposable disposable)
    {
        disposable.Clear();
    }

    public override void Shoot()
    {
    }
}