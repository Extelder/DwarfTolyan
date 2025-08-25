using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using Random = UnityEngine.Random;

public abstract class PlayerCharacteristicBase : MonoBehaviour
{
    public abstract float MinValue { get; set; }
    public abstract float MaxValue { get; set; }
    public abstract float CurrentValue { get; set; }
    public abstract void SetValue(float value);
    public abstract void AddValue(float value);
    public abstract void RemoveValue(float value);
    public abstract void Generate();

    public abstract event Action<float> ValueChanged;
}

public abstract class PlayerCharacteristic<T> : PlayerCharacteristicBase where T : MonoBehaviour
{
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
            // Destroy duplicate instances
            Debug.LogWarning($"Multiple instances of {typeof(T).Name} detected. Destroying duplicate.");
            return;
        }

        Instance = this as T;
        Generate();
    }

    private void Start()
    {
        ValueChanged?.Invoke(CurrentValue);
        OnValueChanged(CurrentValue);
    }

    public override void SetValue(float value)
    {
        CurrentValue = value;
        ValueChanged?.Invoke(CurrentValue);
        OnValueChanged(value);
    }

    public override void AddValue(float value)
    {
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
    [field: SerializeField] public Transform PlayerTransform { get; private set; }
    [field: SerializeField] public Rigidbody Rigidbody { get; private set; }

    public static PlayerCharacter Instance { get; private set; }

    public PlayerBinds Binds { get; private set; }


    private void Awake()
    {
        if (!Instance)
        {
            Binds = new PlayerBinds();

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