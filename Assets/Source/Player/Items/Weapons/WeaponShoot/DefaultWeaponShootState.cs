using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultWeaponShootState : WeaponShootState
{
    public event Action ShootPerformed;

    private bool _alreadyShooting;

    public override void Enter()
    {
        CanChanged = false;
        Animator.Shoot();
    }

    private void Start()
    {
        PerformShoot();
    }

    public void PerformShoot()
    {
        ShootPerformed?.Invoke();
    }

    public void AnimationEndStartChecking()
    {
        _alreadyShooting = false;
        StopAllCoroutines();
        StartCoroutine(AnimationEndChecking());
    }

    public void AnimationEndWithoutChecking()
    {
        StopAllCoroutines();

        CanChanged = true;
    }

    public void AnimationEndStopChecking()
    {
        StopAllCoroutines();

        if (_alreadyShooting)
            return;

        CanChanged = true;
    }

    private IEnumerator AnimationEndChecking()
    {
        while (true)
        {
            if (!CanShoot)
            {
                CanChanged = true;
                yield break;
            }
            
            if (PlayerCharacter.Instance.Binds.Character.MainShoot.inProgress)
            {
                _alreadyShooting = true;
                Animator.Shoot();
                yield break;
            }

            yield return new WaitForSeconds(0.02f);
        }
    }
}