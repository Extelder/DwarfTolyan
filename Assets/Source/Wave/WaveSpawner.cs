using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


[Serializable]
public struct Spawnable
{
    [field: SerializeField] public GameObject Prefab { get; private set; }
    [field: SerializeField] public int MinToSpawn { get; private set; }
    [field: SerializeField] public int MaxToSpawn { get; private set; }
}

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private Wave _wave;

    [SerializeField] private Spawnable[] _spawnable;
    [SerializeField] private float _defaultRate;
    [SerializeField] private float _rateDecreasePerMinute;
    [SerializeField] private int _waveToStart;

    private float _currentRate;

    private List<GameObject> _spawned = new List<GameObject>();

    private void Start()
    {
        _currentRate = _defaultRate;
    }

    private void OnEnable()
    {
        _wave.Started += OnStarted;
        _wave.Ended += OnEnded;
    }

    private void OnStarted(int value)
    {
        if (value >= _waveToStart)
        {
            StartCoroutine(Spawning());
        }
    }

    private IEnumerator Spawning()
    {
        _spawned.Clear();

        while (true)
        {
            yield return new WaitForSeconds(_currentRate);
            for (int i = 0; i < _spawnable.Length; i++)
            {
                for (int j = 0; j < Random.Range(_spawnable[i].MinToSpawn, _spawnable[i].MaxToSpawn); j++)
                {
                    Vector3 spawnPosition =
                        new Vector3(Random.Range(-_wave.DefaultSpawnRadius.x, _wave.DefaultSpawnRadius.x),
                            _wave.DefaultSpawnRadius.y,
                            Random.Range(-_wave.DefaultSpawnRadius.z, _wave.DefaultSpawnRadius.z));
                    _spawned.Add(Instantiate(_spawnable[i].Prefab, spawnPosition, Quaternion.identity));
                }
            }
        }
    }

    private void OnDisable()
    {
        _wave.Started -= OnStarted;
        _wave.Ended -= OnEnded;
    }

    private void OnEnded(int value)
    {
        if (value >= _waveToStart)
        {
            StopAllCoroutines();

            for (int i = 0; i < _spawned.Count; i++)
            {
                if (_spawned[i] == null)
                    continue;
                Destroy(_spawned[i]);
            }
        }
    }
}