using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackStateMachine : EnemyStateMachine
{
    [SerializeField] private State _attack;

    public void Attack()
    {
        ChangeState(_attack);
    }
}
