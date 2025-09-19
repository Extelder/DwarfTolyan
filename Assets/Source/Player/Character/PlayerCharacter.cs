using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;


[Serializable]
public struct ValueChangable
{
    public float rate;
    public float duration;
    public float value;
}

public abstract class PlayerCharacteristicBase : MonoBehaviour
{
    private CompositeDisposable _disposable = new CompositeDisposable();

    private List<CompositeDisposable> _list = new List<CompositeDisposable>();

    public abstract float MinValue { get; set; }
    public abstract float MaxValue { get; set; }
    public abstract float CurrentValue { get; set; }
    public abstract void SetValue(float value);
    public abstract void AddValue(float value);
    public abstract void RemoveValue(float value);
    public abstract void Generate();

    public virtual void AddValueByRateForDuration(ValueChangable ValueChangable)
    {
        CompositeDisposable disposable = new CompositeDisposable();
        _list.Add(disposable);

        Observable.Timer(TimeSpan.FromSeconds(ValueChangable.duration)).Subscribe(_ =>
        {
            _list.Remove(disposable);
            disposable.Clear();
        }).AddTo(disposable);

        Observable.Interval(TimeSpan.FromSeconds(ValueChangable.rate)).Subscribe(_ => { AddValue(ValueChangable.value); }).AddTo(disposable);
    }

    public abstract event Action<float> ValueChanged;


    private void OnDisable()
    {
        _disposable?.Clear();
    }
}

public abstract class PlayerCharacteristic<T> : PlayerCharacteristicBase where T : PlayerCharacteristicBase
{
    public string Name;

    [field: SerializeField] public override float MinValue { get; set; }
    [field: SerializeField] public override float MaxValue { get; set; }
    [field: SerializeField] public override float CurrentValue { get; set; }


    public override event Action<float> ValueChanged;


    public PlayerCharacter Character { get; private set; }

    public static T Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            return;
        }

        Instance = this as T;
        Generate();
    }

    private void Start()
    {
        if (Instance != this)
        {
            return;
        }

        AudioListener.volume = 0.3f;
        ValueChanged?.Invoke(CurrentValue);
        OnValueChanged(CurrentValue);
    }

    public override void SetValue(float value)
    {
        if (Instance != this)
        {
            Instance.SetValue(value);
            return;
        }

        CurrentValue = value;
        ValueChanged?.Invoke(CurrentValue);
        OnValueChanged(value);
    }

    public override void AddValue(float value)
    {
        if (Instance != this)
        {
            Instance.AddValue(value);
            return;
        }

        SetValue(CurrentValue + value);
    }

    [Button()]
    public void Add()
    {
        AddValue(1);
    }

    [Button()]
    public void Remove()
    {
        RemoveValue(1);
    }

    public override void RemoveValue(float value)
    {
        if (Instance != this)
        {
            Instance.RemoveValue(value);
            return;
        }

        SetValue(CurrentValue - value);
    }

    public abstract void OnValueChanged(float value);

    public override void Generate()
    {
        OnValueChanged(CurrentValue);
    }
}

public class PlayerCharacter : MonoBehaviour
{
    [field: SerializeField] public Transform[] PointsAround { get; private set; }
    [field: SerializeField] public Turret[] Turrets { get; private set; }
    [field: SerializeField] public Transform PlayerTransform { get; private set; }
    [field: SerializeField] public Rigidbody Rigidbody { get; private set; }

    public static PlayerCharacter Instance { get; private set; }

    public PlayerBinds Binds { get; private set; }


    private void Awake()
    {
        if (!Instance)
        {
            Binds = InputManager.inputActions;
            Binds.Enable();

            Instance = this;
            return;
        }

        Debug.LogError("There`s one more PlayerCharacter");
    }


    private void OnDisable()
    {
        Binds.Dispose();
        Binds.Disable();
    }
}