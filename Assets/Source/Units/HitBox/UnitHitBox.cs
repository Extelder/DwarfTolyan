using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UniRx;
using UnityEngine;
using Object = System.Object;
using Random = UnityEngine.Random;

public class UnitHitBox : MonoBehaviour, IWeaponVisitor
{
    [SerializeField] private DynamicTextData _damageTextData;

    [SerializeField] private Health _health;

    [SerializeField] private float _damageCooldown = .1f;
    [SerializeField] private float _burningTime = 5;
    [SerializeField] private float _stunCooldown = 2;

    [SerializeField] private bool _notStun;

    [HideIf(nameof(_notStun))] [SerializeField]
    private MonoBehaviour _stunnable;

    public IStunnableStateMachine StunnableStateMachine { get; private set; }

    public event Action Hit;

    public static event Action UnitHitted;

    private CompositeDisposable _disposable = new CompositeDisposable();

    private void Awake()
    {
        if (!_notStun)
            StunnableStateMachine = (IStunnableStateMachine) _stunnable;
    }

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
        Debug.LogError(hit);
        DamageTextFromHit(raycastWeaponShoot.Weapon.DamagePerHit, hit);
        Hit?.Invoke();
        UnitHitted?.Invoke();
    }

    private void DamageTextFromHit(float damage, RaycastHit hit)
    {
        Vector3 destination =
            hit.point + (transform.position - hit.point) / Vector3.Distance(hit.point, transform.position);
        destination.x += (Random.value - 0.5f) / 3f;
        destination.y += Random.value;
        destination.z += (Random.value - 0.5f) / 3f;
        DynamicTextManager.CreateText(destination, damage.ToString(), _damageTextData);
    }

    private void DamageTextFromPoint(float damage, Vector3 point)
    {
        Vector3 destination = point + (transform.position - point) / Vector3.Distance(point, transform.position);
        destination.x += (Random.value - 0.5f);
        destination.y += 3 * Random.value;
        destination.z += (Random.value - 0.5f);
        DynamicTextManager.CreateText(destination, damage.ToString(), _damageTextData);
    }

    public void Visit(Projectile projectile)
    {
        if (!_health)
            return;
        if (_health.IsDead())
            return;
        TakeDamage(projectile.Damage);
        SpawningDecal(projectile.transform.position);
        DamageTextFromPoint(projectile.Damage, projectile.transform.position);
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
        SpawningDecal(transform.position);
        DamageTextFromPoint(weaponOverlapAttack.Damage, transform.position);
        Hit?.Invoke();
        UnitHitted?.Invoke();
    }

    public void Visit(ShieldAttack shieldAttack)
    {
        if (!_health)
            return;
        if (_health.IsDead())
            return;
        TakeDamage(shieldAttack.Damage);
        StartCoroutine(Stun());
        SpawningDecal(transform.position);
        DamageTextFromPoint(shieldAttack.Damage, transform.position);
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
        SpawningDecal(laserGunShoot.CurrentHit.point);
        DamageTextFromHit(damage, laserGunShoot.CurrentHit);
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

    private void SpawningDecal(Vector3 spawnPoint)
    {
        Pools.Instance.BloodExplodeDecalPool.GetFreeElement(spawnPoint, Quaternion.identity);
    }

    private void TakeDamage(float damage)
    {
        _health.TakeDamage(damage);
    }

    private IEnumerator TakeDamageWithCooldown(float damage)
    {
        Observable.Interval(TimeSpan.FromSeconds(_damageCooldown)).Subscribe(_ => { _health.TakeDamage(damage); })
            .AddTo(_disposable);
        yield return new WaitForSeconds(_burningTime);
        StopAllCoroutines();
        _disposable.Clear();
    }

    private IEnumerator Stun()
    {
        StunnableStateMachine.Stun();
        yield return new WaitForSeconds(_stunCooldown);
        StunnableStateMachine.UnStun();
    }
}