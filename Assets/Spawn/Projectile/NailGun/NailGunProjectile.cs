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
}