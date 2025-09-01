using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyNavMeshRandomTurretMove : EnemyNavMeshMove
{
    protected override void AwakeVirtual()
    {
        targetPosition = Character.Turrets[Random.Range(0, Character.Turrets.Length)].transform;
        StartMove();
    }
}