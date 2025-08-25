using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJump : MonoBehaviour
{
    [SerializeField] private GroundChecker _groundChecker;
    [SerializeField] private float _jumpMaxTime;

    private Rigidbody _rigidbody;
    private PlayerBinds _binds;

    private bool _jumping;

    private void Start()
    {
        _binds = PlayerCharacter.Instance.Binds;
        _rigidbody = PlayerCharacter.Instance.Rigidbody;

        _binds.Character.Jump.started += JumpKeyDowned;
        _binds.Character.Jump.canceled += JumpKeyUpped;
    }

    private void OnDisable()
    {
        StopAllCoroutines();
        _binds.Character.Jump.started -= JumpKeyDowned;
        _binds.Character.Jump.canceled -= JumpKeyUpped;
    }

    private void JumpKeyUpped(InputAction.CallbackContext obj)
    {
        StopAllCoroutines();
        _jumping = false;
    }

    private void JumpKeyDowned(InputAction.CallbackContext obj)
    {
        if (_groundChecker.Detected)
            Jump();
    }

    private void Jump()
    {
        _jumping = true;

        StopAllCoroutines();
        StartCoroutine(Jumping());
        StartCoroutine(CancelingJump());
    }

    private IEnumerator Jumping()
    {
        float jumpForce = JumpCharacteristic.Instance.CurrentValue;

        while (_jumping)
        {
            yield return new WaitForSeconds(0.02f);
            _rigidbody.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }
    }

    private IEnumerator CancelingJump()
    {
        yield return new WaitForSeconds(_jumpMaxTime);
        _jumping = false;
        StopAllCoroutines();
    }
}