using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerCharacter))]
public class MoveCharacteristic : PlayerCharacteristic<MoveCharacteristic>
{
    public override void OnValueChanged(float value)
    {
    }
}