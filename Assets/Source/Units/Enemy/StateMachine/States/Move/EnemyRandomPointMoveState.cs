using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyRandomPointMoveState : EnemyMoveState
{
    [SerializeField] private float _walkRadius;
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private EnemyAttackStateMachine _enemyStateMachine;
    [SerializeField] private AudioSource _walkAudio;

    private NavMeshHit _hit;
    

    public override void Enter()
    {
        _walkAudio.Play();
        StartCoroutine(ChooseRandomPoint());
    }
    
    public override void Exit()
    {
        _walkAudio.Stop();
        StopAllCoroutines();
    }

    private IEnumerator ChooseRandomPoint()
    {
        Vector3 randomDirection = Random.insideUnitSphere * _walkRadius;
        randomDirection += transform.position;
        NavMesh.SamplePosition(randomDirection, out _hit, _walkRadius, 1);
        Vector3 finalPosition = _hit.position;
        _agent.destination = finalPosition;
        while (true)
        {
            yield return new WaitForSeconds(0.02f);
            if (_agent.remainingDistance <= 1)
            {
                _enemyStateMachine.Attack();    
            }
        }
    }
}
