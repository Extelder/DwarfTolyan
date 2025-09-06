using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuyableBuff : BuyableBuffBase
{
    [field: SerializeField] public override Item CurrentItem { get; protected set; }

    private void Start()
    {
        SetItem(CurrentItem);
    }

    public override void OnBought()
    {
        CurrentItem.Buy();
    }
}