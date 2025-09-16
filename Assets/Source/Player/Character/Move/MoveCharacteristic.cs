using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCharacteristic : PlayerCharacteristic<MoveCharacteristic>
{
    public override void OnValueChanged(float value)
    {
        Debug.LogError("AAAAAAAAAAAAA ZZZZZZZ" + CurrentValue);
    }
}