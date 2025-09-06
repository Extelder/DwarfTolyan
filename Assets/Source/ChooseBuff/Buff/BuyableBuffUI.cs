using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyableBuffUI : MonoBehaviour
{
    [SerializeField] private Image _iconImage;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _descText;
    [SerializeField] private TextMeshProUGUI _costText;

    [SerializeField] private BuyableBuffBase _buyableBuff;

    private void OnEnable()
    {
        _buyableBuff.Bootstrapped += OnBoostrapped;
    }

    private void OnBoostrapped()
    {
        _nameText.text = _buyableBuff.CurrentItem.name;
        _descText.text = _buyableBuff.CurrentItem.Desc;

        _iconImage.sprite = _buyableBuff.CurrentItem.Icon;
        _costText.text = _buyableBuff.CurrentItem.Cost.ToString();
    }

    private void OnDisable()
    {
        _buyableBuff.Bootstrapped -= OnBoostrapped;
    }
}