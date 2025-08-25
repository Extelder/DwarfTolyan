using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretHeadRotation : MonoBehaviour
{
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
                continue;
            }

            Debug.Log("Visitopr");

            if (other.TryGetComponent<IWeaponVisitor>(out IWeaponVisitor weaponVisitor))
            {
                if (Physics.Raycast(transform.position, (other.transform.position - transform.position),
                    out RaycastHit hit, _searchEnemyDistance, ~_ignoreEnemiesRaycastMask))
                {
                    if (hit.collider.TryGetComponent<IWeaponVisitor>(
                        out IWeaponVisitor visitor))
                    {
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
            transform.LookAt(_currentCollider.ClosestPoint(transform.position), transform.up);
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