using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RpgGunInstance : MonoBehaviour
{
    public RocketGunShoot currentRocketGunShoot;

    public static RpgGunInstance Instance { get; private set; }

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            currentRocketGunShoot = new RocketGunShoot();
            currentRocketGunShoot = new MegaRocketGunShoot(currentRocketGunShoot);
            currentRocketGunShoot.SetInstance();
            currentRocketGunShoot = new MegaRocketGunShoot(currentRocketGunShoot);
            currentRocketGunShoot.SetInstance();
            currentRocketGunShoot.Shoot();
        }
    }
}