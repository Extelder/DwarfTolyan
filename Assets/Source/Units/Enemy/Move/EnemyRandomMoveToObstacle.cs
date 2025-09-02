using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyRandomMoveToObstacle : EnemyMove
{
    [SerializeField] private float _maxDistance;
    [SerializeField] private float _minRemainingDistance;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private Transform _rayOrigin;
    
    [SerializeField] private NavMeshAgent _agent;
    
    private Vector3 _currentRaycastOffset;
    
    private CompositeDisposable _moveDisposable = new CompositeDisposable();

    protected override void AwakeVirtual()
    {
        base.AwakeVirtual();
        ChooseRandomObstacle();
    }

    private void ChooseRandomObstacle()
    {
        _moveDisposable?.Clear();
        _rayOrigin.localEulerAngles = new Vector3(0, Random.Range(-360, 360), 0);
        bool originRaycast = Physics.Raycast(transform.position,  _rayOrigin.forward, 
            out RaycastHit hit, _maxDistance, _layerMask);
        Debug.DrawRay(transform.position,  _rayOrigin.forward * _maxDistance, Color.red);
        if (originRaycast)
        {
            if (hit.collider.TryGetComponent<Wall>(out Wall wall))
            {
                NavMeshMove(hit.point);
            }
        }
    }

    private void NavMeshMove(Vector3 targetPos)
    {
        Debug.Log("move");
        _agent.SetDestination(targetPos);
        DestinatedCheck();
    }

    private void DestinatedCheck()
    {
        Observable.Interval(TimeSpan.FromSeconds(0.02f)).Subscribe(_ =>
        {
            if (_agent.remainingDistance <= _minRemainingDistance)
            {
                Debug.Log("destinated");
                ChooseRandomObstacle();
            }
        }).AddTo(_moveDisposable);
    }

    private void OnDisable()
    {
        _moveDisposable.Clear();
    }
}
