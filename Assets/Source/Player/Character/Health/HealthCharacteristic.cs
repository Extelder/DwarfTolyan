using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCharacteristic : PlayerCharacteristic<HealthCharacteristic>
{
    [SerializeField] private PlayerHealth _health;
    
    public override void OnValueChanged(float value)
    {
        _health.MaxValue = value;
    }
}