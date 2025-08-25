using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretMoveData : MonoBehaviour, ITurretMoveData
{
    [SerializeField] private float _speed;
    [SerializeField] private float _updateDestination;

    public float GetUpdateDestinationRate()
    {
        return _updateDestination;
    }

    public float GetMoveSpeed()
    {
        return _speed;
    }
}