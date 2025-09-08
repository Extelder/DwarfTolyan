using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNavmeshRandomTurretMoveState : EnemyNavmeshMoveState
{
    protected override void AwakeVirtual()
    {
        targetPosition = Character.Turrets[Random.Range(0, Character.Turrets.Length)].transform;
        StartMove();
    }
}
