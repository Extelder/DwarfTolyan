using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UniRx;
using UnityEngine;
using UnityEngine.Events;


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
    [SerializeReference] [SerializeReferenceButton] [SerializeField]
    private CallBack[] _buffsChangeByGlobalEvent;

    [SerializeField] private float _divisionFactorForEach;

    [field: SerializeField] public int AddValue { get; private set; }
    [field: SerializeField] public PlayerCharacteristicBase Characteristic { get; private set; }

    public void OnBought(int spawned)
    {
        for (int i = 0; i < _buffsChangeByGlobalEvent.Length; i++)
        {
            _buffsChangeByGlobalEvent[i].SubscribeToEvent();
        }

        float addValue = AddValue;
        if (spawned != 0)
            addValue = AddValue / (_divisionFactorForEach * spawned);

        Characteristic.AddValue(addValue);
    }

    public void OnDisable()
    {
        for (int i = 0; i < _buffsChangeByGlobalEvent.Length; i++)
        {
            _buffsChangeByGlobalEvent[i].UncribeToEvent();
        }
    }
}

public enum ChangeType
{
    Permanently,
    ByDelay,
    ByRateForDuration
}

[Serializable]
public class CharacteristicValueChange
{
    [field: SerializeField] public PlayerCharacteristicBase Characteristic { get; private set; }
    [field: SerializeField] public float Addible { get; private set; }
    [field: SerializeField] public ChangeType ChangeType { get; private set; }

    private bool _byDelay => ChangeType == ChangeType.ByDelay;
    private bool _byRate => ChangeType == ChangeType.ByRateForDuration;

    [ShowIf(nameof(_byDelay))] [SerializeField]
    private double _delay;

    [ShowIf(nameof(_byRate))] [SerializeField]
    private double _rate;

    private CompositeDisposable _disposable = new CompositeDisposable();

    public void BeginValueChanging()
    {
        switch (ChangeType)
        {
            case ChangeType.Permanently:
                Characteristic.AddValue(Addible);
                break;
            case ChangeType.ByDelay:
                Observable.Timer(TimeSpan.FromSeconds(_delay)).Subscribe(_ => { Characteristic.AddValue(Addible); })
                    .AddTo(_disposable);
                break;
            case ChangeType.ByRateForDuration:
                Observable.Timer(TimeSpan.FromSeconds(_delay)).Subscribe(_ => { _disposable.Clear(); })
                    .AddTo(_disposable);
                Observable.Interval(TimeSpan.FromSeconds(_rate)).Subscribe(_ => { Characteristic.AddValue(Addible); })
                    .AddTo(_disposable);
                break;
        }
    }
}


[CreateAssetMenu(fileName = "Buff/PlayerItem")]
public class PlayerItem : Item
{
    private void OnDisable()
    {
        for (int i = 0; i < _playerCharactersicBuff.Length; i++)
        {
            _playerCharactersicBuff[i].OnDisable();
        }

        _spawned = 0;
    }

    [SerializeField] private PlayerCharactersicBuff[] _playerCharactersicBuff;

    private int _spawned = 0;

    public void WaveStarted()
    {
        Debug.LogError("Wave");
    }

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