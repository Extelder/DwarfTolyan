using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponOverlapAttack : WeaponShoot
{
    public float Damage { get; set; }

    [SerializeField] private OverlapSettings _overlapSettings;

    private void OnEnable()
    {
        WeaponShootState.ShootPerformed += OnShootPerformed;
    }

    private void Overlap()
    {
        _overlapSettings.Colliders = new Collider[10];
        _overlapSettings.Size = Physics.OverlapSphereNonAlloc(
            _overlapSettings.OverlapPoint.position + _overlapSettings.PositionOffset,
            _overlapSettings.SphereRadius, _overlapSettings.Colliders,
            _overlapSettings.SearchLayer);
    }

    public virtual void OnShootPerformed()
    {
        Damage = DamageCharacterics.Instance.CurrentValue;
        Overlap();
        foreach (var other in _overlapSettings.Colliders)
        {
            if (other == null)
                continue;
            if (other.TryGetComponent<TurretHitBox>(out TurretHitBox TurretHitBox))
            {
                TurretHitBox.Repare();
            }

            if (other.TryGetComponent<IWeaponVisitor>(out IWeaponVisitor weaponVisitor))
            {
                Debug.Log("visittttt");
                weaponVisitor.Visit(this);
            }
        }
    }
    
    public void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(_overlapSettings.OverlapPoint.position, _overlapSettings.SphereRadius);
    }

    private void OnDisable()
    {
        WeaponShootState.ShootPerformed -= OnShootPerformed;
    }
}