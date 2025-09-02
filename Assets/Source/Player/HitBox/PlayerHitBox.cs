using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerHitBox : MonoBehaviour
{
    [SerializeField] private float _notActiveDelayAfterSpawn;
    
    private bool _active;
    private PlayerHealth _health;

    private void Start()
    {
        _health = PlayerHealth.Instance;
        StopAllCoroutines();
        StartCoroutine(WaitForDelay());
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<EnemyTriggerAttack>(out EnemyTriggerAttack enemyTriggerAttack))
        {
            _health.TakeDamage(enemyTriggerAttack.Damage);
        }
    }

    private IEnumerator WaitForDelay()
    {
        yield return new WaitForSeconds(_notActiveDelayAfterSpawn);
        _active = true;
    }

    public virtual void TakeDamage(float damage)
    {
        if (!_active)
            return;
        _health.TakeDamage(damage);
    }
}