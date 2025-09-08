using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : StateMachine
{
    [SerializeField] private State _move;

    public void Move()
    {
        ChangeState(_move);
    }
}
