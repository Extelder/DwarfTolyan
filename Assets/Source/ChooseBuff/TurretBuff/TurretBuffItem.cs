using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TurretBuffType
{
    MachineGun,
    LaserGun,
    RpgGun,
    FlameGun
}
[CreateAssetMenu(fileName = "TurretBuffItem")]
public class TurretBuffItem : Item
{
    [SerializeReference] [SerializeReferenceButton]
    private BaseTurretShootAbility _baseTurretShootAbility;

    [SerializeField] private TurretBuffType _buffType;

    public override void Buy()
    {
        base.Buy();
        switch (_buffType)
        {
            case TurretBuffType.MachineGun:
                MachineGunInstance.Instance.ModifyShoot((MachineGunShoot) _baseTurretShootAbility);
                break;
            case TurretBuffType.LaserGun:
                LaserGunInstance.Instance.ModifyShoot((LaserGunShoot) _baseTurretShootAbility);
                break;
            case TurretBuffType.RpgGun:
                RpgGunInstance.Instance.ModifyShoot((RocketGunShoot) _baseTurretShootAbility);
                break;
            case TurretBuffType.FlameGun:
                FlameGunInstance.Instance.ModifyShoot((FlamethrowerShoot) _baseTurretShootAbility);
                break;
        }
    }
}