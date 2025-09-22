using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class ShieldAttack : WeaponOverlapAttack
{
    [SerializeField] private float _checkRate;

    [SerializeField] private PlayerDash _playerDash;

    private CompositeDisposable _disposable = new CompositeDisposable();

    private void Start()
    {
        _playerDash.IsDashing.Subscribe(dash =>
        {
            if (dash)
                StartCoroutine(Attacking());
            else
                StopAllCoroutines();
        }).AddTo(_disposable);
    }

    protected override void VirtualOnDisable()
    {
        StopAllCoroutines();
    }

    public override void OnShootPerformed()
    {
        _playerDash.Dash();
    }

    private IEnumerator Attacking()
    {
        while (true)
        {
            yield return new WaitForSeconds(_checkRate);
            Damage = DamageCharacterics.Instance.CurrentValue;
            Overlap();
            foreach (var other in OverlapSettings.Colliders)
            {
                if (other == null)
                    continue;
                if (other.TryGetComponent<IWeaponVisitor>(out IWeaponVisitor weaponVisitor))
                {
                    weaponVisitor.Visit(this);
                }
            }
        }
    }
}