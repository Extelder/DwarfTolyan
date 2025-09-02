using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretHeadRotation : MonoBehaviour
{
    [SerializeField] private float _speed;
    
    [SerializeField] private float _searchingRange;

    [SerializeField] private LayerMask _ignoreEnemiesRaycastMask;
    [SerializeField] private LayerMask _enemiesMask;
    [SerializeField] private float _searchEnemyDistance;
    [SerializeField] private float _searchEnemyRate;

    private Collider[] _colliders = new Collider[20];

    private float _lastHitDistance = Single.MaxValue;
    private Collider _currentCollider;

    public void SearchNearestEnemy()
    {
        _lastHitDistance = Single.MaxValue;
        _colliders = new Collider[20];
        Physics.OverlapSphereNonAlloc(transform.position, _searchingRange, _colliders, _enemiesMask);
        foreach (var other in _colliders)
        {
            if (other == null)
            {
                _lastHitDistance = Single.MaxValue;
                continue;
            }

            Debug.LogError(other);

            if (other.TryGetComponent<IWeaponVisitor>(out IWeaponVisitor weaponVisitor))
            {
                if (Physics.Raycast(transform.position, (other.transform.position - transform.position),
                    out RaycastHit hit, _searchEnemyDistance, ~_ignoreEnemiesRaycastMask))
                {
                    Debug.DrawRay(transform.position, (other.transform.position - transform.position) * _searchEnemyDistance);
                    if (hit.collider.TryGetComponent<IWeaponVisitor>(
                        out IWeaponVisitor visitor))
                    {
                        Debug.LogError(visitor);
                        if (_lastHitDistance > hit.distance)
                        {
                            _lastHitDistance = hit.distance;
                            _currentCollider = other;
                        }
                    }
                }
            }
        }
    }


    private void Start()
    {
        StartCoroutine(SearchingForEnemy());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private void Update()
    {
        if (_currentCollider != null)
        {
            Vector3 direction = _currentCollider.ClosestPoint(transform.position) - transform.position;
            Quaternion toRotation = Quaternion.LookRotation(direction, transform.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, _speed * Time.deltaTime);
        }
    }

    private IEnumerator SearchingForEnemy()
    {
        while (true)
        {
            yield return new WaitForSeconds(_searchEnemyRate);
            SearchNearestEnemy();
        }
    }
}