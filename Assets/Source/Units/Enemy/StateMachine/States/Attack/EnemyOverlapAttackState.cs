using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOverlapAttackState : EnemyAttackState
{
    [SerializeField] private EnemyStateMachine _stateMachine;
    [SerializeField] private Animator _animator;
    [SerializeField] private string _attackAnimationBoolName;
    
    public override void Enter()
    {
        _animator.SetBool(_attackAnimationBoolName, true);
    }

    public override void OnEnableVirtual()
    {
        base.OnEnableVirtual();
        PlayerCheck.PlayerLost += OnPlayerLost;
    }

    public override void OnPlayerDetected(PlayerHitBox hitBox)
    {
        base.OnPlayerDetected(hitBox);
    }

    public override void PerformAttack()
    {
        base.PerformAttack();
        Debug.Log("damageDef");
    }

    private void OnPlayerLost()
    {
        _animator.SetBool(_attackAnimationBoolName, false);
        _stateMachine.Move();
    }

    public override void OnDisableVirtual()
    {
        base.OnDisableVirtual();
        PlayerCheck.PlayerLost -= OnPlayerLost;
    }
}
