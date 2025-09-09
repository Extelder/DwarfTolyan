using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponOverheatMaterialChange : MonoBehaviour
{
    [SerializeField] private WeaponOverheat _overheat;

    [SerializeField] private MeshRenderer _meshRenderer;

    private Color _emmisionColor;

    private Material _material;

    private void OnEnable()
    {
        _overheat.CurrentValueChanged += OnCurrentValueChanged;
    }

    private void Start()
    {
        _material = _meshRenderer.materials[1];
        _emmisionColor = _material.GetColor("_EmissionColor");
    }

    private void OnCurrentValueChanged(float value)
    {
        _material.SetColor("_EmissionColor", _emmisionColor * (value * 10));
    }

    private void OnDisable()
    {
        _overheat.CurrentValueChanged -= OnCurrentValueChanged;
    }
}