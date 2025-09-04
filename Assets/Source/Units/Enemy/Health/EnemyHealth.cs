using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{
    public override void Death()
    {
        Pools.Instance.CoinPool.GetFreeElement(transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}