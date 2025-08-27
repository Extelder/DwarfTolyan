using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGunShoot : TurretShootAbility
{
    [SerializeField] protected Transform _shootOrigin;
    [SerializeField] protected float _range;
    [SerializeField] protected LayerMask _layerMask;
    [SerializeField] protected Turret _turret;

    private RaycastHit[] _hits;
    
    public override void Shoot()
    {
        Vector3 direction = _shootOrigin.position + _shootOrigin.forward * _range;
        _hits = Physics.RaycastAll(_shootOrigin.position, direction, _layerMask);
        for (int i = 0; i < _hits.Length; i++)
        {
            RaycastHit hit = _hits[i];
            if (_hits[i].collider == null)
            {
                continue;
            }
            if (hit.collider.TryGetComponent<IWeaponVisitor>(out IWeaponVisitor visitor))
            {
                visitor.Visit(this, _turret.Damage);
            }
        }
    }
}
