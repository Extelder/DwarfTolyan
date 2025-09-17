using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAvoidPlayerMoveState : EnemyMoveState
{
    [SerializeField] private EnemyStateMachine _stateMachine;
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private EnemyOverlapPlayerCheck _check;
    [SerializeField] private RaycastSettings _raycastSettings;
    [SerializeField] private float _updatePositionRate;

    private CompositeDisposable _disposable = new CompositeDisposable();
    
    private Ray _forwardRay;
    private Ray _downRay;

    private void OnEnable()
    {
        _check.PlayerDetected += OnPlayerDetected;
        _check.PlayerLost += OnPlayerLost;
    }

    private void OnPlayerLost()
    {
        _disposable?.Clear();
        _stateMachine.Idle();
    }

    private void OnPlayerDetected(PlayerHitBox hitBox)
    {
        _forwardRay = new Ray(_raycastSettings.RayOrigin.position,   _raycastSettings.RayOrigin.position + transform.forward - Character.PlayerTransform.position);
        Debug.DrawRay(_raycastSettings.RayOrigin.position,
            (_raycastSettings.RayOrigin.position + transform.forward - Character.PlayerTransform.position) * _raycastSettings.MaxDistance, Color.yellow, 2f);
        Vector3 hitPoint = _forwardRay.GetPoint(_raycastSettings.MaxDistance);
        _downRay = new Ray(hitPoint, -transform.up);
        Debug.DrawRay(hitPoint, -transform.up * _raycastSettings.MaxDistance, Color.red, 2f);
        if (Physics.Raycast(_forwardRay, out RaycastHit forwardHit, _raycastSettings.MaxDistance, _raycastSettings.LayerMask))
        {
            if (forwardHit.collider.TryGetComponent<Wall>(out Wall wall))
            {
                ThrowRaycastDown();
                return;
            }
        }
        ThrowRaycastDown();
    }

    private void ThrowRaycastDown()
    {
        if (Physics.Raycast(_downRay, out RaycastHit hit, _raycastSettings.MaxDistance, _raycastSettings.LayerMask))
        {
            if (hit.collider == null)
            {
                return;
            }
            if (hit.collider.TryGetComponent<Ground>(out Ground ground))
            {
                SetDestination(hit.point);
            }
        }
    }

    private void SetDestination(Vector3 destination)
    {
        _disposable.Clear();
        Observable.Interval(TimeSpan.FromSeconds(_updatePositionRate)).Subscribe(_ =>
        {
            _agent.SetDestination(destination);
        }).AddTo(_disposable);
    }

    public override void Enter()
    {
    }

    private void OnDisable()
    {
        _disposable.Clear();
        _check.PlayerDetected -= OnPlayerDetected;
        _check.PlayerLost -= OnPlayerLost;
    }
}
