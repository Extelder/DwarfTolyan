using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretLock : MonoBehaviour
{
    [SerializeField] private BuyableBuffBase _buff;

    [SerializeField] private GameObject _lockObject;

    private void OnCountChanged(int count)
    {
        CheckForOverflow(count);
    }

    private void OnEnable()
    {
        _buff.Bootstrapped += OnBootstrapped;
    }

    private void OnBootstrapped()
    {
        if (_buff.CurrentItem is TurretBuyItem)
        {
            CheckForOverflow(TurretItemSpawner.Instance.CurrentCount);
            TurretItemSpawner.Instance.CountChanged += OnCountChanged;
        }
    }

    private void CheckForOverflow(int value)
    {
        if (value >= 6)
        {
            Lock();
        }
        else
        {
            UnLock();
        }
    }

    public void Lock()
    {
        if (_lockObject != null)
            _lockObject?.SetActive(true);
    }

    public void UnLock()
    {
        if (_lockObject != null)
            _lockObject?.SetActive(false);
    }

    private void OnDestroy()
    {
        _buff.Bootstrapped -= OnBootstrapped;
        TurretItemSpawner.Instance.CountChanged -= OnCountChanged;
    }
}