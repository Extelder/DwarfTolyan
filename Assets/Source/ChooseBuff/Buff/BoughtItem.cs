using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoughtItem : MonoBehaviour
{
    [SerializeField] private Image _icon;

    private bool _boostrapped;

    public void Bootstrap(Item item)
    {
        _icon.sprite = item.Icon;
        _boostrapped = true;
    }

}