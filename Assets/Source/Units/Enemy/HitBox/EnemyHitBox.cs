using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class EnemyHitBox : UnitHitBox
{
    public override IEnumerator Stun()
    {
        StunnableStateMachine.Stun();
        return base.Stun();
    }
}
   
