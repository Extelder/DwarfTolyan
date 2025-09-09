using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStunnableAttackStateMachine : EnemyAttackStateMachine, IStunnableStateMachine
{
    [field: SerializeField] public State StunState { get; set; }
    [field: SerializeField] public State UnStunState { get; set; }

    public void Stun()
    {
        Debug.Log(CurrentState + "State");
        Debug.Log(CurrentState.CanChanged + "Change");
        if (CurrentState != StunState)
            CurrentState.CanChanged = true;
        ChangeState(StunState);
    }

    public void UnStun()
    {
        StunState.CanChanged = true;
        ChangeState(UnStunState);
    }
}