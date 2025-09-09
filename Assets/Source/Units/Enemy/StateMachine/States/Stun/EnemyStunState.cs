using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
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

    public void UnStun()
    {
        CanChanged = true;
        _agent.isStopped = false;
    }
}
