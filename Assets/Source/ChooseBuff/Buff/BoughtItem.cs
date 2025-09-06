using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoughtItem : MonoBehaviour
{
    [SerializeField] private Image _icon;

    public void Bootstrap(Item item)
    {
        _icon.sprite = item.Icon;
    }
}