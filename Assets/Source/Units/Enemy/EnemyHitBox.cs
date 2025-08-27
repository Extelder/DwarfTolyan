using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitBox : MonoBehaviour, IWeaponVisitor
{
    public void Visit(WeaponShoot weaponShoot)
    {
        
    }

    public void Visit(RaycastWeaponShoot raycastWeaponShoot, RaycastHit hit)
    {
        Debug.LogError(raycastWeaponShoot.Weapon.DamagePerHit);
    }

    public void Visit(Projectile projectile)
    {
        Debug.LogError(projectile.Damage);
    }

    public void Visit(LaserGunShoot laserGunShoot, float damage)
    {
        Debug.LogError(damage);
    }

    public void Visit(FlamethrowerShoot flamethrowerShoot, float damage)
    {
        Debug.LogError(damage);
    }
}
   
