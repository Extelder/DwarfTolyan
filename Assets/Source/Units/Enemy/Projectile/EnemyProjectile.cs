using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : PoolObject
{
    [SerializeField] private float _speed;
    [SerializeField] private Rigidbody _rigidbody;

    [SerializeField] private float _damage;

    private void Update()
    {
        _rigidbody.velocity = transform.forward * _speed;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent<TurretHitBox>(out TurretHitBox TurretHitBox))
        {
            gameObject.SetActive(false);
            return;
        }

        if (other.gameObject.TryGetComponent<PlayerHitBox>(out PlayerHitBox PlayerHitBox))
        {
            PlayerHitBox.TakeDamage(_damage);
        }

        gameObject.SetActive(false);
    }
}