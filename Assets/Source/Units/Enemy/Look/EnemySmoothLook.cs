using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySmoothLook : MonoBehaviour
{
    [SerializeField] private float _speed;
    private PlayerCharacter _character;
    private Transform _characterTransform;

    private void Start()
    {
        _character = PlayerCharacter.Instance;
        _characterTransform = _character.PlayerTransform;
    }

    private void Update()
    {
        Vector3 direction = _characterTransform.position - transform.position;
        Quaternion toRotation = Quaternion.LookRotation(direction, transform.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, _speed * Time.deltaTime);
    }
}