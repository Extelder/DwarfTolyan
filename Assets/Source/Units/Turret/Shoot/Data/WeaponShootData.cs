using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShootData : MonoBehaviour, IWeaponShootData
{
    [SerializeField] private int _damage;
    [SerializeField] private float _rate;

    public WeaponShootData(IWeaponShootData weaponShootData)
    {
        _damage = weaponShootData.GetDamage();
    }
    
    public int GetDamage()
    {
        return _damage;
    }

    public float GetRate()
    {
        return _rate;
    }
}
