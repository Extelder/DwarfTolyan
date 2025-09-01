using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlyingMove : EnemyMove
{
    [SerializeField] private float _stopDistance;

    [SerializeField] private float _speed;

    [SerializeField] private Rigidbody _rigidbody;

    private void Update()
    {
        Vector3 _newPosition = transform.position;
        _newPosition.y += Mathf.Sin(Time.time) * Time.deltaTime;
        transform.position = _newPosition;

        if (Vector3.Distance(transform.position, Character.PlayerTransform.position) > _stopDistance)
            _rigidbody.velocity = transform.forward.normalized * _speed;
        else
            _rigidbody.velocity = new Vector3(0, 0 , 0);
    }
}