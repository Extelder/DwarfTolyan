using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxHealthCharacteristic : PlayerCharacteristic<MaxHealthCharacteristic>
{
    [SerializeField] private PlayerHealth _health;
    
    public override void OnValueChanged(float value)
    {
        _health.MaxValue = value;
    }
}