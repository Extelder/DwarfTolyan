using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyEnemyStunState : State
{
    [SerializeField] private FlyEnemyMoveState _enemyMove;
    public override void Enter()
    {
        CanChanged = false;
        _enemyMove.StopMove();
    }
}
