using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldAttack : WeaponOverlapAttack
{
    [SerializeField] private PlayerDash _playerDash;
    
    public override void OnShootPerformed()
    {
        base.OnShootPerformed();
        _playerDash.Dash();
    }
}
