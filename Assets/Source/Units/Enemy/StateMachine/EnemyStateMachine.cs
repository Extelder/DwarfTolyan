using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class EnemyStateMachine : StateMachine
{
    [SerializeField] private bool _canIdle;
    [SerializeField] private State _move;
    [ShowIf(nameof(_canIdle))]
    [SerializeField] private State _idle;

    public void Move()
    {
        ChangeState(_move);
    }

    public void Idle()
    {
        if (_canIdle)
            ChangeState(_idle);
    }
}
