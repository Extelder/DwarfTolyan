using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{
    [SerializeField] private int _coinsDropped = 4;

    private bool _spawned;

    public override void Death()
    {
        if (_spawned)
            return;
        _spawned = true;
        for (int i = 0; i < _coinsDropped; i++)
        {
            Pools.Instance.CoinPool.GetFreeElement(transform.position + new Vector3(.1f * _coinsDropped, 0, 0),
                Quaternion.identity);
        }

        Destroy(gameObject);
    }
}