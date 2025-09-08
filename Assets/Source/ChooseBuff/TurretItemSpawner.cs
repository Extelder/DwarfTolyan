using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretItemSpawner : MonoBehaviour
{
    [SerializeField] private BoughtItem _prefab;
    [SerializeField] private Transform _parent;

    public static TurretItemSpawner Instance { get; private set; }

    public BoughtItem[] _BoughtItems = new BoughtItem[6];

    public int CurrentCount { get; private set; }

    public event Action<int> CountChanged;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            return;
        }

        Debug.LogError("One more ItemSpawner");
    }

    public void SpawnItem(Item item)
    {
        CurrentCount++;
        CountChanged?.Invoke(CurrentCount);
        BoughtItem instance = Instantiate(_prefab, _parent);
        instance.Bootstrap(item);
    }
}