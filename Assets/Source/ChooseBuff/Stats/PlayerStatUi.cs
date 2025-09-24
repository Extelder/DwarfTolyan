using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerStatUi : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _valueText;
    [SerializeField] private TextMeshProUGUI _nameText;

    [SerializeField] private PlayerBuffType _buffType;


    private void OnEnable()
    {
        switch (_buffType)
        {
            case PlayerBuffType.Damage:
                _nameText.text = PlayerCharacteristic<DamageCharacterics>.Instance.Name;
                PlayerCharacteristic<DamageCharacterics>.Instance.ValueChanged += ValueChanged;
                ValueChanged(PlayerCharacteristic<DamageCharacterics>.Instance.CurrentValue);
                break;
            case PlayerBuffType.Hp:
                _nameText.text = PlayerCharacteristic<MaxHealthCharacteristic>.Instance.Name;
                PlayerCharacteristic<MaxHealthCharacteristic>.Instance.ValueChanged += ValueChanged;
                ValueChanged(PlayerCharacteristic<MaxHealthCharacteristic>.Instance.CurrentValue);
                break;
            case PlayerBuffType.MovementSpeed:
                _nameText.text = PlayerCharacteristic<MoveCharacteristic>.Instance.Name;
                PlayerCharacteristic<MoveCharacteristic>.Instance.ValueChanged += ValueChanged;
                ValueChanged(PlayerCharacteristic<MoveCharacteristic>.Instance.CurrentValue);
                break;
            case PlayerBuffType.ShootSpeed:
                _nameText.text = PlayerCharacteristic<AttackSpeedCharacteristic>.Instance.Name;
                PlayerCharacteristic<AttackSpeedCharacteristic>.Instance.ValueChanged += ValueChanged;
                ValueChanged(PlayerCharacteristic<AttackSpeedCharacteristic>.Instance.CurrentValue);
                break;
        }
    }

    private void ValueChanged(float value)
    {
        _valueText.text = value.ToString();
    }

    private void OnDisable()
    {
        switch (_buffType)
        {
            case PlayerBuffType.Damage:
                PlayerCharacteristic<DamageCharacterics>.Instance.ValueChanged -= ValueChanged;
                break;
            case PlayerBuffType.Hp:
                PlayerCharacteristic<MaxHealthCharacteristic>.Instance.ValueChanged -= ValueChanged;
                break;
            case PlayerBuffType.MovementSpeed:
                PlayerCharacteristic<MoveCharacteristic>.Instance.ValueChanged -= ValueChanged;
                break;
            case PlayerBuffType.ShootSpeed:
                PlayerCharacteristic<AttackSpeedCharacteristic>.Instance.ValueChanged -= ValueChanged;
                break;
        }
    }
}