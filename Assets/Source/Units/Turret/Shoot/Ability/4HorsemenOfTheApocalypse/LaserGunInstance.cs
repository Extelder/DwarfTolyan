using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGunInstance : MonoBehaviour
{
    public LaserGunShoot CurrentLaserGunShoot;

    public static LaserGunInstance Instance { get; private set; }

    public void ModifyShoot(LaserGunShoot modificator)
    {
        var type = modificator.GetType();

        var newInstance = Activator.CreateInstance(type, CurrentLaserGunShoot);
        CurrentLaserGunShoot = (LaserGunShoot) newInstance;
        CurrentLaserGunShoot.SetInstance();
    }
    
    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            CurrentLaserGunShoot = new LaserGunShoot();
            CurrentLaserGunShoot.SetInstance();
        }
    }
}
