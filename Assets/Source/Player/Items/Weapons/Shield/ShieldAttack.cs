using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class ShieldAttack : WeaponOverlapAttack
{
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

    public override void OnShootPerformed()
    {
        _playerDash.Dash();
    }

    private IEnumerator Attacking()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            Damage = DamageCharacterics.Instance.CurrentValue;
            Overlap();
            foreach (var other in OverlapSettings.Colliders)
            {
                if (other == null)
                    continue;
                Debug.Log(other.name + "collider");
                if (other.TryGetComponent<IWeaponVisitor>(out IWeaponVisitor weaponVisitor))
                {
                    Debug.Log(weaponVisitor + "visitor");
                    weaponVisitor.Visit(this);
                }
            }
        }
    }
}
