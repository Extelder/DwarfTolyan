using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameGunInstance : MonoBehaviour
{
    public FlamethrowerShoot currentRocketGunShoot;

    public static FlameGunInstance Instance { get; private set; }

    public void ModifyShoot(FlamethrowerShoot modificator)
    {
        var type = modificator.GetType();

        var newInstance = Activator.CreateInstance(type, currentRocketGunShoot);
        currentRocketGunShoot = (FlamethrowerShoot) newInstance;
    }
    
    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            currentRocketGunShoot = new FlamethrowerShoot();
            currentRocketGunShoot.SetInstance();
        }
    }
}
