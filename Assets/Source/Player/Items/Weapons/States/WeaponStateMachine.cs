using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponStateMachine : StateMachine
{
    [field: SerializeField] public ItemTakeUp Item { get; private set; }

    [Header("States")] [SerializeField] private State _idle;
    [SerializeField] private WeaponShootState _shoot;

    public override void OnEnable()
    {
        base.OnEnable();

        PlayerCharacter.Instance.Binds.Character.MainShoot.started += OnMainShootPressedDown;
        PlayerCharacter.Instance.Binds.Character.MainShoot.canceled += OnMainShootPressedUp;
    }
    
    private void OnMainShootPressedDown(InputAction.CallbackContext obj)
    {
        StopAllCoroutines();

        StartCoroutine(WaitingForTakeUpToShoot());
    }
    
    private void OnMainShootPressedUp(InputAction.CallbackContext obj)
    {
        StopAllCoroutines();
        if (_shoot.CanShoot == false)
            ChangeState(_idle);
        StartCoroutine(TryingToExitShoot());
    }

    private IEnumerator WaitingForTakeUpToShoot()
    {
        while (true)
        {
            if (_shoot.CanShoot == false)
            {
                ChangeState(_idle);
                break;
            }

            if (Item.TakeUpped && _shoot.CanShoot)
                ChangeState(_shoot);
            yield return new WaitForSeconds(0.05f);
        }
    }

    private IEnumerator TryingToExitShoot()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.02f);
            ChangeState(_idle);
        }
    }

    public virtual void OnDisable()
    {
        PlayerCharacter.Instance.Binds.Character.MainShoot.started -= OnMainShootPressedDown;
        PlayerCharacter.Instance.Binds.Character.MainShoot.canceled -= OnMainShootPressedUp;
    }
}