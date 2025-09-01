using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileShootAttack : MonoBehaviour
{
    [SerializeField] private float _rate;
    [SerializeField] private float _burstRate;
    [SerializeField] private int _burst;
    [SerializeField] private Transform _spawnPlace;

    private void Start()
    {
        StartCoroutine(Shooting());
    }

    private IEnumerator Shooting()
    {
        while (true)
        {
            yield return new WaitForSeconds(_rate);

            for (int i = 0; i < _burst; i++)
            {
                Pools.Instance.FlyEnemyProjectilePool
                    .GetFreeElement(_spawnPlace.position, transform.rotation);
                yield return new WaitForSeconds(_burstRate);
            }
        }
    }
}