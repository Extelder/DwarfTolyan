using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UniRx;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public abstract class CallBack
{
    public Action InstanceReceived;

    public abstract void SetInstance();

    public virtual void InvokeIfActive()
    {
        InstanceReceived.Invoke();
    }

    public virtual void SubscribeToEvent()
    {
        InstanceReceived += EventInvoked;
    }

    public virtual void UncribeToEvent()
    {
        InstanceReceived -= EventInvoked;
    }

    public abstract void EventInvoked();
}

[Serializable]
public abstract class GlobalCallBack<T> : CallBack where T : CallBack
{
    public static T Instance { get; protected set; }

    public override void SubscribeToEvent()
    {
        Instance.InstanceReceived += EventInvoked;
    }

    public override void UncribeToEvent()
    {
        Instance.InstanceReceived -= EventInvoked;
    }

    public abstract override void SetInstance();
    public abstract override void EventInvoked();
}

[Serializable]
public class WaveCallBack : GlobalCallBack<WaveCallBack>
{
    [SerializeField] private ValueChangable _value;

    public UnityEvent<ValueChangable> Event;

    public override void SetInstance()
    {
        Instance = this;
    }

    public override void EventInvoked()
    {
        Debug.LogError(_value);
        Event?.Invoke(_value);
    }
}


public class GlobalEvents : MonoBehaviour
{
    public static event Action<int> WaveStarted;
    public static event Action<int> WaveEnded;

    [SerializeReference] [SerializeReferenceButton]
    private CallBack[] _allCallBacks;

    private void Awake()
    {
        for (int i = 0; i < _allCallBacks.Length; i++)
        {
            _allCallBacks[i].SetInstance();
        }
    }

    private void Start()
    {
        Wave.Instance.Started += OnWaveStarted;
        Wave.Instance.Ended += OnWaveEnded;
    }

    private void OnDisable()
    {
        Wave.Instance.Started -= OnWaveStarted;
        Wave.Instance.Ended -= OnWaveEnded;
    }

    public void OnWaveEnded(int wave)
    {
        WaveEnded?.Invoke(wave);
    }

    public void OnWaveStarted(int wave)
    {
        WaveStarted?.Invoke(wave);
    }
}