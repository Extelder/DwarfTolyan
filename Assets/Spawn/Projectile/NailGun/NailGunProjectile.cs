using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class NailGunProjectile : Projectile
{
    public override void Initiate(Vector3 targetPosition, bool use = true)
    {
        base.Initiate(targetPosition);
    }

    public override void Initiate(Vector3 targetPosition, float damage, bool useTargetPosition = true)
    {
        base.Initiate(targetPosition, damage);
    }

    protected override void VirtualOnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent<TurretHitBox>(out TurretHitBox TurretHitBox))
        {
            Debug.Log(TurretHitBox);
            TurretHitBox.Repare(34);
        }
    }

    protected override void ExplodeIteration(Collider other)
    {
        if (other.gameObject.TryGetComponent<TurretHitBox>(out TurretHitBox TurretHitBox))
        {
            Debug.Log(TurretHitBox);
            TurretHitBox.Repare(34);
        }
    }
}