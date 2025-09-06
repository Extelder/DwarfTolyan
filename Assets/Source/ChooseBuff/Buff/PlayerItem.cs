using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PlayerBuffType
{
    Damage,
    Hp,
    MovementSpeed,
    ShootSpeed
}

[CreateAssetMenu(fileName = "Buff/PlayerItem")]
public class PlayerItem : Item
{
    [field: SerializeField] public int AddValue { get; private set; }

    [field: SerializeField] public PlayerBuffType BuffType { get; private set; }

    public override void Buy()
    {
        base.Buy();
        switch (BuffType)
        {
            case PlayerBuffType.Damage:
                DamageCharacterics.Instance.AddValue(AddValue);
                break;
            case PlayerBuffType.Hp:
                HealthCharacteristic.Instance.AddValue(AddValue);
                break;
            case PlayerBuffType.MovementSpeed:
                MoveCharacteristic.Instance.AddValue(AddValue);
                break;
            case PlayerBuffType.ShootSpeed:
                AttackSpeedCharacteristic.Instance.AddValue(AddValue);
                break;
        }
    }
}