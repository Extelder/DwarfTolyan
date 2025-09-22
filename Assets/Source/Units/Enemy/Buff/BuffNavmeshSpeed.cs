using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BuffNavmeshSpeed : MonoBehaviour ,IBuffable
{
    [SerializeField] private NavMeshAgent _agent;

    public void Buff(float buffValue)
    {
        _agent.speed += buffValue;
    }
}
