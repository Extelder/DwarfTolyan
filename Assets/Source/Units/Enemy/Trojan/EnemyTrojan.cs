using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyTrojan : MonoBehaviour
{
    [SerializeField] private GameObject _enemyParent;

    [SerializeField] private GameObject _enemyToSpawn;
    [SerializeField] private int _count;
    [SerializeField] private Transform _spawnPoint;

    public void SpawnEnemies()
    {
        for (int i = 0; i < _count; i++)
        {
            Instantiate(_enemyToSpawn, _spawnPoint.position - new Vector3(Random.Range(-1, 1), 0, Random.Range(-1, 1)),
                Quaternion.identity);
            Destroy(_enemyParent);
        }
    }
}