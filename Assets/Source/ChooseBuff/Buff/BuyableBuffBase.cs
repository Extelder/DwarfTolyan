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

    public bool Locked { get; private set; }

    public void SetItem(Item item)
    {
        CurrentItem = item;
        Bootstrapped?.Invoke();
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