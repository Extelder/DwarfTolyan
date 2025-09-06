using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShootData : MonoBehaviour, IWeaponShootData
{
    [SerializeField] private float _rate;

    public WeaponShootData(IWeaponShootData weaponShootData)
    {
        _rate = weaponShootData.GetRate();
    }

    public float GetRate()
    {
        return _rate;
    }
}
