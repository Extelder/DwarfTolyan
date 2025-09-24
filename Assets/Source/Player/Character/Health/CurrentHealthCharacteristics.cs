using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentHealthCharacteristics : PlayerCharacteristic<CurrentHealthCharacteristics>
{
    [SerializeField] private PlayerHealth _health;
    public override void OnValueChanged(float value)
    {
        _health.SetCurrentValue(CurrentValue);
    }
}