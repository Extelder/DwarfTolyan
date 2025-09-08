using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

[Serializable]
public struct RaycastSettings
{
    [field: SerializeField] public float MaxDistance { get; private set; }
    [field: SerializeField] public LayerMask LayerMask { get; private set; }
    [field: SerializeField] public Transform RayOrigin { get; private set; }
}

public class EnemyMoveToObstaclesState : EnemyMoveState
{
    [SerializeField] private float _minRemainingDistance;
    [SerializeField] private RaycastSettings _raycastSettings;
    [SerializeField] private NavMeshAgent _agent;
    
    private CompositeDisposable _moveDisposable = new CompositeDisposable();

    public override void Enter()
    {
        ChooseRandomObstacle();
    }

    private void ChooseRandomObstacle()
    {
        _moveDisposable?.Clear();
        _raycastSettings.RayOrigin.localEulerAngles = new Vector3(0, Random.Range(-360, 360), 0);
        bool originRaycast = Physics.Raycast(transform.position, _raycastSettings.RayOrigin.forward,
            out RaycastHit hit, _raycastSettings.MaxDistance, _raycastSettings.LayerMask);
        Debug.DrawRay(transform.position, _raycastSettings.RayOrigin.forward * _raycastSettings.MaxDistance, Color.red);
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
        _agent.SetDestination(targetPos);
        DestinatedCheck();
    }

    private void DestinatedCheck()
    {
        Observable.Interval(TimeSpan.FromSeconds(0.02f)).Subscribe(_ =>
        {
            if (_agent.remainingDistance <= _minRemainingDistance)
            {
                ChooseRandomObstacle();
            }
        }).AddTo(_moveDisposable);
    }

    private void OnDisable()
    {
        _moveDisposable.Clear();
    }
}