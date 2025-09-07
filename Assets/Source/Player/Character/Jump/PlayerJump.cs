using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJump : MonoBehaviour
{
    [SerializeField] private GroundChecker _groundChecker;

    private Rigidbody _rigidbody;
    private PlayerBinds _binds;

    private bool _jumping;

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
        if (_groundChecker.Detected)
            Jump();
    }

    private void Jump()
    {
        float jumpForce = JumpCharacteristic.Instance.CurrentValue;

        if (_groundChecker.Detected)
            _rigidbody.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
}