using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTriggerInteract : MonoBehaviour
{
    [SerializeField] private PlayerCharacter _character;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<ITriggerInteractable>(out ITriggerInteractable triggerInteractable))
        {
            triggerInteractable.Trigger(_character);
        }
    }
}