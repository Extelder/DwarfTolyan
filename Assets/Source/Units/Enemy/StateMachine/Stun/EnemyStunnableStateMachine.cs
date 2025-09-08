using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStunnableStateMachine : EnemyStateMachine, IStunnableStateMachine
{
    [field: SerializeField] public State StunState { get; set; }
    [field: SerializeField] public State UnStunState { get; set; }

    public void Stun()
    {
        ChangeState(StunState);
    }

    public void UnStun()
    {
        Debug.Log("exitmain");
        StunState.CanChanged = true;
        ChangeState(UnStunState);
    }
}
