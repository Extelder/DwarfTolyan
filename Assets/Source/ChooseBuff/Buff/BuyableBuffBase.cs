using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class BuyableBuffBase : MonoBehaviour
{
    public abstract Item CurrentItem { get; protected set; }

    public event Action Bootstrapped;
    public event Action Bought;

    [field: SerializeField] public float Cost { get; private set; }

    public bool Locked { get; private set; }

    private bool _bootstrap;

    public void SetItem(Item item)
    {
        CurrentItem = item;
        Cost = item.Cost;
        Cost *= Wave.Instance.CostMultiplier;
        _bootstrap = true;
        Bootstrapped?.Invoke();
    }

    private void OnEnable()
    {
        Debug.LogError(Wave.Instance.CostMultiplier);

        if (_bootstrap)
        {
            Debug.LogError(Wave.Instance.CostMultiplier);
            Cost *= Wave.Instance.CostMultiplier;
        }
    }

    public void LockUnlock(TextMeshProUGUI text)
    {
        Locked = !Locked;
        text.text = Locked ? "Locked" : "Lock";
    }

    public void Buy()
    {
        OnBought();
        Bought?.Invoke();
        Destroy(gameObject);
    }

    public abstract void OnBought();
}