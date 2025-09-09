using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RpgGunInstance : MonoBehaviour
{
    public RocketGunShoot currentRocketGunShoot;

    public static RpgGunInstance Instance { get; private set; }

    public void ModifyShoot(RocketGunShoot modificator)
    {
        var type = modificator.GetType();

        var newInstance = Activator.CreateInstance(type, currentRocketGunShoot);
        currentRocketGunShoot = (RocketGunShoot) newInstance;
        currentRocketGunShoot.SetInstance();
    }
    
    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            currentRocketGunShoot = new RocketGunShoot();
            currentRocketGunShoot.SetInstance();
        }
    }
}