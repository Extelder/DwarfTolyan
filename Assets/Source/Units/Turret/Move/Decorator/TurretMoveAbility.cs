using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class TurretMoveAbility : BaseTurretMove
{
    [field: SerializeField] public override TurretMoveData MoveData { get; protected set; }

    public override void StartMoving(ref CompositeDisposable disposable, Action Move)
    {
        Observable.Interval(TimeSpan.FromSeconds(MoveData.GetUpdateDestinationRate()))
            .Subscribe(_ => { Move?.Invoke(); }).AddTo(disposable);
    }

    public override void Move()
    {
    }
}