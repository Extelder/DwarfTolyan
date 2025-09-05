using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretProjectile : Projectile
{
    public override void Initiate(Vector3 targetPosition, bool use = true)
    {
        base.Initiate(targetPosition);
    }

    public override void Initiate(Vector3 targetPosition, float damage, bool useTargetPosition = true)
    {
        base.Initiate(targetPosition, damage);
    }

    protected override void VirtualOnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<TurretHitBox>(out TurretHitBox TurretHitBox))
        {
            return;
        }

        base.VirtualOnTriggerEnter(other);
    }
}