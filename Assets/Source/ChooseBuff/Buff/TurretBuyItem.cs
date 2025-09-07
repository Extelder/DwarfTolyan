using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Buff/TurretItem")]
public class TurretBuyItem : Item
{
    [SerializeField] private GameObject _turret;

    public override void Buy()
    {
        TurretSpawner.Instance.SpawnItem(this);
        Instantiate(_turret,
            PlayerCharacter.Instance.PointsAround[Random.Range(0, PlayerCharacter.Instance.PointsAround.Length)]
                .position, Quaternion.identity);
    }
}