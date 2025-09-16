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

[Serializable]
public struct PlayerCharactersicBuff
{
    [SerializeField] private float _divisionFactorForEach;

    [field: SerializeField] public int AddValue { get; private set; }
    [field: SerializeField] public PlayerCharacteristicBase Characteristic { get; private set; }

    public void OnBought(int spawned)
    {
        float addValue = AddValue;
        if (spawned != 0)
            addValue = AddValue / (_divisionFactorForEach * spawned);

        Characteristic.AddValue(addValue);
    }
}

[CreateAssetMenu(fileName = "Buff/PlayerItem")]
public class PlayerItem : Item
{
    private void OnDisable()
    {
        _spawned = 0;
    }

    [SerializeField] private PlayerCharactersicBuff[] _playerCharactersicBuff;

    private int _spawned = 0;


    public override void Buy()
    {
        base.Buy();
        _spawned++;
        for (int i = 0; i < _playerCharactersicBuff.Length; i++)
        {
            _playerCharactersicBuff[i].OnBought(_spawned);
        }
    }
}