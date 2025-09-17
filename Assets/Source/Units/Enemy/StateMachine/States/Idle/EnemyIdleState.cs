using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : State
{
    [SerializeField] private Animator _animator;
    [SerializeField] private string _boolName;
    public override void Enter()
    {
        _animator.SetBool(_boolName, true);
    }

    public override void Exit()
    {
        _animator.SetBool(_boolName, false);
    }
}
