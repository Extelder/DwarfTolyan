using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private GroundChecker _groundChecker;
    [SerializeField] private float _acceleration;
    [SerializeField] private float _decceleration;

    private Rigidbody _rigidbody;
    private PlayerBinds _binds;
    private Transform _transform;

    private CompositeDisposable _disposable = new CompositeDisposable();

    private MoveCharacteristic _moveCharacteristic;

    private Vector3 _currentVelocity;

    public bool Moving { get; private set; }

    private void Start()
    {
        _rigidbody = PlayerCharacter.Instance.Rigidbody;
        _binds = PlayerCharacter.Instance.Binds;
        _transform = PlayerCharacter.Instance.PlayerTransform;

        _moveCharacteristic = MoveCharacteristic.Instance;

        Vector3 inputVector = new Vector3(0, 0, 0);

        Observable.EveryUpdate().Subscribe(_ =>
        {
            inputVector = new Vector3(_binds.Character.Horizontal.ReadValue<float>(), 0,
                _binds.Character.Vertical.ReadValue<float>());
        }).AddTo(_disposable);

        Observable.EveryFixedUpdate().Subscribe(_ =>
        {
            inputVector = transform.TransformDirection(inputVector);

            inputVector.Normalize();

            Moving = Mathf.Abs(inputVector.x) > 0 || Mathf.Abs(inputVector.z) > 0;

            Vector3 desiredVelocityXZ = new Vector3(inputVector.x * _moveCharacteristic.CurrentValue, 0,
                inputVector.z * _moveCharacteristic.CurrentValue);

            if (Moving == true || _groundChecker.Detected == true)
                _currentVelocity =
                    Vector3.MoveTowards(_currentVelocity, desiredVelocityXZ, _acceleration * Time.deltaTime);
            else if (Moving == false)
            {
                _currentVelocity =
                    Vector3.MoveTowards(_currentVelocity, desiredVelocityXZ, _decceleration * Time.deltaTime);
            }

            _rigidbody.velocity =
                new Vector3(_currentVelocity.x, _rigidbody.velocity.y, _currentVelocity.z);
        }).AddTo(_disposable);
    }

    private void OnDisable()
    {
        _disposable?.Clear();
    }
}