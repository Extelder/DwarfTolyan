using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class LaserGunShoot : TurretShootAbility
{
    [SerializeField] protected Transform _shootOrigin;
    [SerializeField] protected float _range;
    [SerializeField] protected LayerMask _layerMask;
    [SerializeField] protected Turret _turret;

    [SerializeField] public bool _instance;

    private CompositeDisposable _disposable = new CompositeDisposable();
    private RaycastHit[] _hits;

    public RaycastHit CurrentHit { get; private set; }

    public virtual void Bootstrap(Transform origin, float range, Turret turret, LayerMask layerMask)
    {
        _shootOrigin = origin;
        _range = range;
        _turret = turret;
        _layerMask = layerMask;
    }

    public static LaserGunShoot Instance { get; private set; }

    public void SetInstance()
    {
        _instance = true;
        Instance = this;
    }


    public override void Shoot()
    {
        if (_instance == false)
        {
            Instance.Bootstrap(_shootOrigin, _range, _turret, _layerMask);
            Instance.Shoot();
            return;
        }

        Vector3 direction = _shootOrigin.position + _shootOrigin.forward * _range;
        _hits = Physics.RaycastAll(_shootOrigin.position, direction, _layerMask);
        Debug.Log("shoot");
        Pools.Instance.TrailPool.GetFreeElement(_shootOrigin.position, _shootOrigin.rotation);
        for (int i = 0; i < _hits.Length; i++)
        {
            RaycastHit hit = _hits[i];
            if (_hits[i].collider == null)
            {
                continue;
            }

            CurrentHit = hit;
            if (hit.collider.TryGetComponent<IWeaponVisitor>(out IWeaponVisitor visitor))
            {
                visitor.Visit(this, _turret.Damage);
            }
        }
    }
}