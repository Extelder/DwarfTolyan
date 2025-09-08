using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStunnableStateMachine
{
    public State StunState { get; set; }
    public void Stun();
}
