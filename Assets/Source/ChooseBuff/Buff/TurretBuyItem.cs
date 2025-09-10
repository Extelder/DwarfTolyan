using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Buff/TurretItem")]
public class TurretBuyItem : Item
{
    [SerializeField] private GameObject _turret;

    public override void Buy()
    {
        TurretItemSpawner.Instance.SpawnItem(this, _turret.GetComponent<Turret>());
    }
}