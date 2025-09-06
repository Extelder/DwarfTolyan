using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretShootSound : MonoBehaviour
{
    [SerializeField] private AudioSource _source;

    [SerializeField] private Turret _turret;

    private void OnEnable()
    {
        _turret.Shooted += OnShooted;
    }

    private void OnShooted()
    {
        _source.Play();
    }

    private void OnDisable()
    {
        _turret.Shooted -= OnShooted;
    }
}