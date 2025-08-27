using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using DG.Tweening;
using UniRx;
using UnityEngine;

public class Projectile : PoolObject
{
    [SerializeField] private CinemachineImpulseSource _cinemachineImpulseSource;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private GameObject _projectileGFX;
    [field: SerializeField] public float Damage { get; private set; }
    [field: SerializeField] public float ExplosionForce { get; private set; }
    [field: SerializeField] public float ExplosionRange { get; set; }
    [SerializeField] private bool _onlyPlayerHealth;
    [SerializeField] private float _speed;

    private Collider[] _colliders = new Collider[50];
    private Collider _collider;
    private bool _explosived;
    private bool _useGravity;
    public CompositeDisposable Disposable { get; private set; } = new CompositeDisposable();

    public event Action Exploded;

    [field: SerializeField] public Rigidbody Rigidbody { get; private set; }

    private void Awake()
    {
        _useGravity = Rigidbody.useGravity;
        _collider = GetComponent<Collider>();
    }

    public virtual void Initiate(Vector3 targetPosition, bool useTargetPosition = true)
    {
        StopAllCoroutines();
        _collider.enabled = true;
        _projectileGFX.SetActive(true);
        Rigidbody.useGravity = _useGravity;
        Rigidbody.velocity = new Vector3(0, 0, 0);
        if (useTargetPosition)
            transform.LookAt(targetPosition, transform.forward);
        Rigidbody.AddForce(transform.forward * _speed, ForceMode.Impulse);
    }

    public virtual void Initiate(Vector3 targetPosition, float damage, bool useTargetPosition = true)
    {
        Damage = damage;
        Initiate(targetPosition, useTargetPosition);
    }

    private void OnDisable()
    {
        Disposable.Clear();
        _explosived = false;
        OnDisableVirtual();
    }

    public virtual void OnDisableVirtual()
    {
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.material.bounciness >= 0.95f)
            return;
        ;
        if (other.gameObject.TryGetComponent<Projectile>(out Projectile projectile))
            return;
        if (_onlyPlayerHealth)
            Explode();

        if (!other.gameObject.TryGetComponent<PlayerMovement>(out PlayerMovement movement) && !_onlyPlayerHealth)
            Explode();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.material.bounciness >= 0.95f)
            return;
        if (other.TryGetComponent<Projectile>(out Projectile projectile))
            return;
        if (_onlyPlayerHealth)
            Explode();

        if (!other.TryGetComponent<PlayerMovement>(out PlayerMovement movement) && !_onlyPlayerHealth)
            Explode();
    }


    public void Explode()
    {
        Disposable.Clear();
        if (_explosived)
            return;
        _collider.enabled = false;

        Rigidbody.useGravity = false;
        Rigidbody.velocity = new Vector3(0, 0, 0);

        _cinemachineImpulseSource?.GenerateImpulse();
        _explosived = true;
        _colliders = new Collider[35];
        Physics.OverlapSphereNonAlloc(transform.position, ExplosionRange, _colliders, _layerMask);

        foreach (var other in _colliders)
        {
            if (!other)
                continue;
            if (other.TryGetComponent<IWeaponVisitor>(out IWeaponVisitor visitor))
            {
                if (_onlyPlayerHealth)
                    continue;
                Accept(visitor);
                continue;
            }

            if (other.TryGetComponent<PlayerHitBox>(out PlayerHitBox playerHitBox))
            {
                if (_onlyPlayerHealth)
                {
                    playerHitBox.TakeDamage(Damage);
                    continue;
                }
            }

            if (other.TryGetComponent<Rigidbody>(out Rigidbody rigidbody))
            {
                rigidbody.AddExplosionForce(ExplosionForce, transform.position, ExplosionRange);
            }
        }


        Exploded?.Invoke();
        _projectileGFX.SetActive(false);
        Invoke(nameof(ReturnToPool), ReturnToPoolDelay);
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, ExplosionRange);
    }

    public virtual void Accept(IWeaponVisitor visitor)
    {
        visitor.Visit(this);
    }
}