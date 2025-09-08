using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyMoveState : State
{
    private void Awake()
    {
        Character  = PlayerCharacter.Instance;
        AwakeVirtual();
    }

    public abstract override void Enter();
    
    protected PlayerCharacter Character;

    protected virtual void AwakeVirtual()
    {
        
    }
}
