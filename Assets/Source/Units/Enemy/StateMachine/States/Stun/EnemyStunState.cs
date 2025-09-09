using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStunState : State
{
    [SerializeField] private Animator _animator;
    [SerializeField] private string _boolName;
    [SerializeField] private NavMeshAgent _agent;
    
    public override void Enter()
    {
        CanChanged = false;
        _agent.isStopped = true;
        Debug.Log("Stun");
        _animator.SetBool(_boolName, true);
    }

    public void UnStun()
    {
        CanChanged = true;
        Debug.Log("UnStun");
        _animator.SetBool(_boolName, false);
        _agent.isStopped = false;
    }
}
