using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnStunState : State
{
    [SerializeField] private EnemyStunState _stunState;

    public override void Enter()
    {
        Debug.Log("unstunstate");
        _stunState.UnStun();
    }
}
