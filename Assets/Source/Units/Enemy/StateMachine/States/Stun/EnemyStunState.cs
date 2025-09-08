using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStunState : State
{
    [SerializeField] private NavMeshAgent _agent;
    
    public override void Enter()
    {
        CanChanged = false;
        _agent.isStopped = true;
    }

    public override void Exit()
    {
        base.Exit();
        _agent.isStopped = false;
    }
}
