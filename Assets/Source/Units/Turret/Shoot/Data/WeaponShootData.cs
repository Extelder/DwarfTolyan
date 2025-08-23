using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShootData : IWeaponShootData
{
    private int _damage;

    public WeaponShootData(IWeaponShootData weaponShootData)
    {
        _damage = weaponShootData.GetDamage();
    }
    
    public int GetDamage()
    {
        return _damage;
    }
}
