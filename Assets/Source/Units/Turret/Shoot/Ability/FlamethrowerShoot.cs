using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class FlamethrowerShoot : TurretShootAbility
{
    [SerializeField] private Transform _shootOrigin;
    [SerializeField] private float _range;
    [SerializeField] private int _colliderCount;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private Turret _turret;

    private Collider[] _colliders;
    public override void Shoot()
    {
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
