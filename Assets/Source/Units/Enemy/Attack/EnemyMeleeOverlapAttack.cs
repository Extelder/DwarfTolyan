using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeOverlapAttack : EnemyMeleeAttack
{
    [SerializeField] private Animator _animator;
    [SerializeField] private string _attackAnimationBoolName;
    
    public override void OnEnableVirtual()
    {
        base.OnEnableVirtual();
        PlayerCheck.PlayerLost += OnPlayerLost;
    }

    public override void OnPlayerDetected(PlayerHitBox hitBox)
    {
        base.OnPlayerDetected(hitBox);
        _animator.SetBool(_attackAnimationBoolName, true);
    }

    public override void PerformAttack()
    {
        base.PerformAttack();
        Debug.Log("damageDef");
    }

    public override void OnPlayerLost()
    {
        _animator.SetBool(_attackAnimationBoolName, false);
    }

    public override void OnDisableVirtual()
    {
        base.OnDisableVirtual();
        PlayerCheck.PlayerLost -= OnPlayerLost;
    }
}
