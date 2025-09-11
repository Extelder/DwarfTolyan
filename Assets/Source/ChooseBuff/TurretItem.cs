using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TurretItem : BoughtItem
{
    [SerializeField] private TextMeshProUGUI _levelText;

    public Turret SpawnedTurret { get; private set; }

    public void Bootstrap(Item item, Turret spawnedTurret)
    {
        Debug.LogError(spawnedTurret);
        SpawnedTurret = spawnedTurret;
        _levelText.text = SpawnedTurret.Level.ToString();
        SpawnedTurret.LevelChanged += OnLevelChanged;
        Bootstrap(item);
    }

    private void OnLevelChanged(int value)
    {
        _levelText.text = value.ToString();
    }

    private void OnDestroy()
    {
        if (SpawnedTurret != null)
            SpawnedTurret.LevelChanged -= OnLevelChanged;
    }

    public void Combine()
    {
        for (int i = 0; i < TurretItemSpawner.Instance.TurretItems.Length; i++)
        {
            if (TurretItemSpawner.Instance.TurretItems[i] == null)
                continue;

            if (TurretItemSpawner.Instance.TurretItems[i] == this)
                continue;

            if (TurretItemSpawner.Instance.TurretItems[i].SpawnedTurret.ShootAbility.GetType() ==
                SpawnedTurret.ShootAbility.GetType())
            {
                if (TurretItemSpawner.Instance.TurretItems[i].SpawnedTurret.Level != SpawnedTurret.Level)
                    continue;

                SpawnedTurret.IncreaseLevel();

                TurretItemSpawner.Instance.DecreseCount();
                Destroy(TurretItemSpawner.Instance.TurretItems[i].SpawnedTurret.gameObject);
                Destroy(TurretItemSpawner.Instance.TurretItems[i].gameObject);

                Debug.LogError("COMBINE REAL");
            }
        }
    }
}