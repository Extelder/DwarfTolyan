using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class Coin : PoolObject, ITriggerInteractable
{
    [SerializeField] private float _moveToPlayerSpeed;

    private CompositeDisposable _disposable = new CompositeDisposable();

    private bool _moving;

    public override void OnEnableVirtual()
    {
        _moving = false;
        base.OnEnableVirtual();
    }

    public void Trigger(PlayerCharacter character)
    {
        if (_moving)
            return;

        _moving = true;

        Observable.EveryUpdate().Subscribe(_ =>
        {
            transform.position =
                Vector3.MoveTowards(transform.position, character.PlayerTransform.position,
                    _moveToPlayerSpeed * Time.deltaTime);
        }).AddTo(_disposable);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerHitBox>(out PlayerHitBox PlayerHitBox))
        {
            PlayerWallet.Instance.Add(1);
            gameObject.SetActive(false);
        }
    }

    public override void OnDisableVirtual()
    {
        _disposable.Clear();
        base.OnDisableVirtual();
    }
}