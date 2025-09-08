using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameGunInstance : MonoBehaviour
{
    public FlamethrowerShoot currentRocketGunShoot;

    public static FlameGunInstance Instance { get; private set; }

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            currentRocketGunShoot = new FlamethrowerShoot();
            currentRocketGunShoot = new MegaFlamethrowerShoot(currentRocketGunShoot);
            currentRocketGunShoot.SetInstance();
            currentRocketGunShoot = new MegaFlamethrowerShoot(currentRocketGunShoot);
            currentRocketGunShoot.SetInstance();
            currentRocketGunShoot.Shoot();
        }
    }
}
