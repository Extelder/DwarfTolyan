using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldHitBox : PlayerHitBox
{
    [SerializeField] private float _damageResist;

    private ShieldReflectionChanceCharacteristics _shieldReflectionChanceCharacteristics;

    private void Start()
    {
        _shieldReflectionChanceCharacteristics = ShieldReflectionChanceCharacteristics.Instance;
    }
    
    public override void TakeDamage(float damage)
    {
        damage /= _damageResist;
        base.TakeDamage(damage);
    }

    public override void TakeDamage(float damage, IReflectionable reflectionable)
    {
        float random = Random.value;
        if (_shieldReflectionChanceCharacteristics.CurrentValue >= random)
        {
            Debug.Log("reflect");
            reflectionable.TakeReflection();
        }
    }
}
