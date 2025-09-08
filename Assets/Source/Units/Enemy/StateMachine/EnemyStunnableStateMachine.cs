using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStunnableStateMachine : EnemyStateMachine, IStunnableStateMachine
{
    [field: SerializeField] public State StunState { get; set; }

    public void Stun()
    {
        ChangeState(StunState);
    }
}
