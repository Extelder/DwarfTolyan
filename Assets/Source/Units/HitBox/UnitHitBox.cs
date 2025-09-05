using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class UnitHitBox : MonoBehaviour, IWeaponVisitor
{
    [SerializeField] private Health _health;
    
    [SerializeField] private float _damageCooldown = .1f;
    [SerializeField] private float _burningTime = 5;
    
    public event Action Hit;

    public static event Action UnitHitted;
    
    private CompositeDisposable _disposable = new CompositeDisposable();
    public void Visit(WeaponShoot weaponShoot)
    {
    }

    public void Visit(RaycastWeaponShoot raycastWeaponShoot, RaycastHit hit)
    {
        if (!_health)
            return;
        if (_health.IsDead())
            return;
        Debug.LogError(raycastWeaponShoot.Weapon.DamagePerHit + " raycastDamage");
        TakeDamage(raycastWeaponShoot.Weapon.DamagePerHit);
        Hit?.Invoke();
        UnitHitted?.Invoke();
    }

    public void Visit(Projectile projectile)
    {
        if (!_health)
            return;
        if (_health.IsDead())
            return;
        TakeDamage(projectile.Damage);
        SpawningDecal(transform.position);
        Hit?.Invoke();
        UnitHitted?.Invoke();
    }

    public void Visit(WeaponOverlapAttack weaponOverlapAttack)
    {
        if (!_health)
            return;
        if (_health.IsDead())
            return;
        TakeDamage(weaponOverlapAttack.Damage);
        Debug.LogError(weaponOverlapAttack.Damage + " melee");
        SpawningDecal(transform.position);
        Hit?.Invoke();
        UnitHitted?.Invoke();
    }

    public void Visit(LaserGunShoot laserGunShoot, float damage)
    {
        if (!_health)
            return;
        if (_health.IsDead())
            return;
        TakeDamage(damage);
        SpawningDecal(transform.position);
        Hit?.Invoke();
        UnitHitted?.Invoke();
    }

    public void Visit(FlamethrowerShoot flamethrowerShoot, float damage)
    {
        if (!_health)
            return;
        if (_health.IsDead())
            return;
        StartCoroutine(TakeDamageWithCooldown(damage));
        SpawningDecal(transform.position);
        Hit?.Invoke();
        UnitHitted?.Invoke();
    }
    
    public virtual void SpawningDecal(Vector3 spawnPoint)
    {
        //var currentObject = Pools.Instance.BloodExplodeDecalPool.GetFreeElement(spawnPoint, Quaternion.identity);
    }
    
    public virtual void TakeDamage(float damage)
    {
        _health.TakeDamage(damage);
    }
    
    public virtual IEnumerator TakeDamageWithCooldown(float damage)
    {
        Observable.Interval(TimeSpan.FromSeconds(_damageCooldown)).Subscribe(_ =>
        {
            Debug.LogError(damage + " burn");
            _health.TakeDamage(damage);
        }).AddTo(_disposable);
        yield return new WaitForSeconds(_burningTime);
        StopAllCoroutines();
        _disposable.Clear();
    }
}
