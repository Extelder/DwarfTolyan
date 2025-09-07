using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKamikzaeAttack : EnemyMeleeAttack
{
    [SerializeField] private GameObject _parent;

    public override void OnPlayerDetected(PlayerHitBox hitBox)
    {
        base.OnPlayerDetected(hitBox);
        PerformAttack();
        Pools.Instance.ExplodeKamikzaePool.GetFreeElement(transform.position, Quaternion.identity);
        Destroy(_parent);
    }

    public override void PerformAttack()
    {
        base.PerformAttack();
        Debug.Log("damageKamikadze");
    }

    public override void OnPlayerLost()
    {
    }
}