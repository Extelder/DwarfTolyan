using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAttackState : State, IReflectionable
{
    [field: SerializeField] public EnemyDamage Damage { get; private set; }
    [field: SerializeField] public EnemyPlayerCheck PlayerCheck { get; private set; }

    public PlayerHitBox PlayerHitBox { get; private set; }

    public abstract override void Enter();

    private void OnEnable()
    {
        PlayerCheck.PlayerDetected += OnPlayerDetected;
        OnEnableVirtual();
    }

    public virtual void OnEnableVirtual()
    {
        
    }

    public virtual void PerformAttack()
    {
        PlayerHitBox.TakeDamage(Damage.GetDamage(), this);
    }

    public virtual void OnPlayerDetected(PlayerHitBox hitBox)
    {
        PlayerHitBox = hitBox;
    }

    public virtual void OnDisableVirtual()
    {
        
    }

    protected virtual void OnDisable()
    {
        PlayerCheck.PlayerDetected -= OnPlayerDetected;
        OnDisableVirtual();
    }

    [field: SerializeField] public EnemyHealth Health { get; set; }
    public void TakeReflection()
    {
        Health.TakeDamage(Damage.GetDamage() / 2);
    }
}
