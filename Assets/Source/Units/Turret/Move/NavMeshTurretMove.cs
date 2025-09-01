using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshTurretMove : TurretMoveAbility
{
    [SerializeField] private NavMeshAgent _agent;

    public override void Move()
    {
        base.Move();
        _agent.speed = MoveData.GetMoveSpeed();
        _agent.SetDestination(PlayerCharacter.Instance
            .PointsAround[Random.Range(0, PlayerCharacter.Instance.PointsAround.Length)].position);
    }

    public override void StopMoving(ref CompositeDisposable disposable)
    {
        base.StopMoving(ref disposable);
        _agent.SetDestination(_agent.transform.position);
    }
}