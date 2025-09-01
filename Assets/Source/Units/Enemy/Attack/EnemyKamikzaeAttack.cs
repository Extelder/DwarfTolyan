using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKamikzaeAttack : MonoBehaviour
{
    [SerializeField] private GameObject _parent;
    [SerializeField] private float _damage;
    [SerializeField] private EnemyPlayerCheck _playerCheck;

    private void OnEnable()
    {
        _playerCheck.PlayerDetected += OnPlayerDetected;
    }

    private void OnPlayerDetected(PlayerHitBox hitBox)
    {
        hitBox.TakeDamage(_damage);
        Pools.Instance.ExplodeKamikzaePool.GetFreeElement(transform.position, Quaternion.identity);
        Destroy(_parent);
    }

    private void OnDisable()
    {
        _playerCheck.PlayerDetected -= OnPlayerDetected;
    }
}