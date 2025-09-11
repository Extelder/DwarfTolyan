using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TurretItemSpawner : MonoBehaviour
{
    [SerializeField] private TurretItem _prefab;
    [SerializeField] private Transform _parent;

    public static TurretItemSpawner Instance { get; private set; }

    public int CurrentCount { get; set; }

    public TurretItem[] TurretItems = new TurretItem[6];

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

    public void DecreseCount()
    {
        CurrentCount--;
        CountChanged?.Invoke(CurrentCount);
    }

    public void SpawnItem(TurretBuyItem item, Turret spawnedTurret)
    {
        CurrentCount++;
        CountChanged?.Invoke(CurrentCount);
        Turret instanceTurret = Instantiate(spawnedTurret,
            PlayerCharacter.Instance.PointsAround[Random.Range(0, PlayerCharacter.Instance.PointsAround.Length)]
                .position, Quaternion.identity).GetComponent<Turret>();

        TurretItem instance = Instantiate(_prefab, _parent);
        instance.Bootstrap(item, instanceTurret);

        for (int i = 0; i < TurretItems.Length; i++)
        {
            if (TurretItems[i] == null)
            {
                TurretItems[i] = instance;
                return;
            }
        }
    }
}