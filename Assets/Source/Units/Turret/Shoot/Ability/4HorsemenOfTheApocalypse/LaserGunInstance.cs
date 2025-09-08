using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGunInstance : MonoBehaviour
{
    public LaserGunShoot CurrentLaserGunShoot;

    public static LaserGunInstance Instance { get; private set; }

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            CurrentLaserGunShoot = new LaserGunShoot();
            CurrentLaserGunShoot = new MegaLaserGunShoot(CurrentLaserGunShoot);
            CurrentLaserGunShoot.SetInstance();CurrentLaserGunShoot = new MegaLaserGunShoot(CurrentLaserGunShoot);
            CurrentLaserGunShoot.SetInstance();
            CurrentLaserGunShoot.Shoot();
        }
    }
}
