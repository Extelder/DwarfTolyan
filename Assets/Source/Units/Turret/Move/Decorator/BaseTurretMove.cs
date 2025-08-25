using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public abstract class BaseTurretMove
{
    [field: SerializeField] public abstract TurretMoveData MoveData { get; protected set; }

    
    public abstract void StartMoving(ref CompositeDisposable disposable, Action Move);

    public abstract void Move();
}
