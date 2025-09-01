using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretHitBox : PlayerHitBox
{
    [SerializeField] private Turret _turret;


    public void Repare()
    {
        _turret.Enable();
    }
    
    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        _turret.Disable();
    }
}