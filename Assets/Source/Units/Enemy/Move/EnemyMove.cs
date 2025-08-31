using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyMove : MonoBehaviour
{
    protected PlayerCharacter Character;

    private void Awake()
    {
        Character  = PlayerCharacter.Instance;
        AwakeVirtual();
    }

    protected virtual void AwakeVirtual()
    {
        
    }
}