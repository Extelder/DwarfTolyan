using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStunnableAttackStateMachine : EnemyAttackStateMachine, IStunnableStateMachine
{
    [field: SerializeField] public State StunState { get; set; }

    public void Stun()
    {
        ChangeState(StunState);
    }
}
