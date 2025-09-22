using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveOverlapPlayerCheck : EnemyOverlapPlayerCheck
{
    public override void OnPlayerDetected()
    {
        StateMachine.Move();
    }
}
