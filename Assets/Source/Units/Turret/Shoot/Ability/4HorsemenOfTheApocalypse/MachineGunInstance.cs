using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGunInstance : MonoBehaviour
{
    public MachineGunShoot CurrentMachineGunShoot;

    public static MachineGunInstance Instance { get; private set; }

    public void ModifyShoot(MachineGunShoot modificator)
    {
        var type = modificator.GetType();

        var newInstance = Activator.CreateInstance(type, CurrentMachineGunShoot);
        CurrentMachineGunShoot = (MachineGunShoot) newInstance;
        CurrentMachineGunShoot.SetInstance();
    }

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            CurrentMachineGunShoot = new MachineGunShoot();
            CurrentMachineGunShoot.SetInstance();
        }
    }
}