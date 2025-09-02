using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretHitBox : PlayerHitBox
{
    [SerializeField] private Turret _turret;

    private float _repairValue = 0;

    private bool _repaired = true;


    public void Repare(float value)
    {
        if (_repaired)
            return;

        _repairValue += value;
        if (_repairValue >= 100)
        {
            _repaired = true;
            _turret.Enable();
            _repairValue = 0;
        }
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        Debug.LogError("TURRET");
        _repaired = false;
        _turret.Disable();
    }
}