using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyCheck : MonoBehaviour
{
    public abstract event Action<EnemyHitBox> EnemyDetected;
    public abstract event Action EnemyLost;
    public abstract void StartChecking();
    public abstract void StopChecking();
}
