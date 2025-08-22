using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJump : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private PlayerBinds _binds;

    private void Start()
    {
        _binds = PlayerCharacter.Instance.Binds;
        _rigidbody = PlayerCharacter.Instance.Rigidbody;

        _binds.Character.Jump.started += JumpKeyDowned;
    }

    private void OnDisable()
    {
        _binds.Character.Jump.started -= JumpKeyDowned;
    }

    private void JumpKeyDowned(InputAction.CallbackContext obj)
    {
        Jump();
    }

    private void Jump()
    {
        float jumpForce = JumpCharacteristic.Instance.CurrentValue;

        _rigidbody.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
}