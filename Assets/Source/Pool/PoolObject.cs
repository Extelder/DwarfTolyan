using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolObject : MonoBehaviour
{
    [field: SerializeField] public float ReturnToPoolDelay { get; private set; } = 2f;
    [SerializeField] private bool _autoreturnToPool = true;

    private void OnEnable()
    {
        if (_autoreturnToPool)
            StartCoroutine(ReturningToPool());
    }

    private IEnumerator ReturningToPool()
    {
        yield return new WaitForSeconds(ReturnToPoolDelay);
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        if (_autoreturnToPool)
            StopAllCoroutines();
    }
}