using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretSpawner : MonoBehaviour
{
    [SerializeField] private BoughtItem _prefab;
    [SerializeField] private Transform _parent;

    public static TurretSpawner Instance { get; private set; }

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
        BoughtItem instance = Instantiate(_prefab, _parent);
        instance.Bootstrap(item);
    }
}
