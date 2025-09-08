using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class FlamethrowerShoot : TurretShootAbility
{
    [SerializeField] public bool _instance;

    [SerializeField] private Transform _shootOrigin;
    [SerializeField] private float _range;
    [SerializeField] private int _colliderCount;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private Turret _turret;

    private Collider[] _colliders;

    public static FlamethrowerShoot Instance { get; private set; }

    public void SetInstance()
    {
        _instance = true;
        Instance = this;
    }

    public virtual void Bootstrap(Transform origin, float range, Turret turret, LayerMask layerMask, int colliderCount)
    {
        _shootOrigin = origin;
        _range = range;
        _turret = turret;
        _colliderCount = colliderCount;
        _layerMask = layerMask;
    }


    public override void Shoot()
    {
        if (_instance == false)
        {
            Instance.Bootstrap(_shootOrigin, _range, _turret, _layerMask, _colliderCount);
            Instance.Shoot();
            return;
        }

        _colliders = new Collider[_colliderCount];
        Physics.OverlapSphereNonAlloc(_shootOrigin.position, _range, _colliders, _layerMask);
        foreach (var other in _colliders)
        {
            if (!other)
            {
                continue;
            }

            if (other.TryGetComponent<IWeaponVisitor>(out IWeaponVisitor visitor))
            {
                visitor.Visit(this, _turret.Damage);
            }
        }
    }

    public override void DrawGizmos()
    {
        base.DrawGizmos();
        Gizmos.DrawWireSphere(_shootOrigin.position, _range);
    }
}