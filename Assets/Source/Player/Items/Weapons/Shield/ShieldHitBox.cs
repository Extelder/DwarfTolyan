using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldHitBox : PlayerHitBox
{
    [SerializeField] private float _damageResist;

    public override void TakeDamage(float damage)
    {
         Debug.Log("player shield Damage " + damage);
        damage /= _damageResist;
        base.TakeDamage(damage);
    }
}
