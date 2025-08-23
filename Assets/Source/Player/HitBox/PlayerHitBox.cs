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

    private void Awake()
    {
        _health = PlayerHealth.Instance;
    }

    private void Start()
    {
        StopAllCoroutines();
        StartCoroutine(WaitForDelay());
    }

    private IEnumerator WaitForDelay()
    {
        yield return new WaitForSeconds(_notActiveDelayAfterSpawn);
        _active = true;
    }

    public void TakeDamage(float damage)
    {
        if (!_active)
            return;
        
        _health.TakeDamage(damage);
    }
}