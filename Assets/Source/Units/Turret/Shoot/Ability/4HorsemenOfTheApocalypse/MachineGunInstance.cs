using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGunInstance : MonoBehaviour
{
    public MachineGunShoot CurrentMachineGunShoot;

    public static MachineGunInstance Instance { get; private set; }

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            CurrentMachineGunShoot = new MachineGunShoot();
            CurrentMachineGunShoot = new MegaMachineGunShoot(CurrentMachineGunShoot);
            CurrentMachineGunShoot.SetInstance();CurrentMachineGunShoot = new MegaMachineGunShoot(CurrentMachineGunShoot);
            CurrentMachineGunShoot.SetInstance();
            CurrentMachineGunShoot.Shoot();
        }
    }
}