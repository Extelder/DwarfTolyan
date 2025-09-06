using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BuffSpawner : MonoBehaviour
{
    [SerializeField] private Transform _parent;

    [SerializeField] private Item[] _items;

    [SerializeField] private BuyableBuffBase _buyableBuffBase;

    private List<BuyableBuffBase> _buyableBuffBases = new List<BuyableBuffBase>();

    private void OnEnable()
    {
        Reroll();
    }

    public void Reroll()
    {
        if (_buyableBuffBases.Count > 0)
        {
            for (int i = 0; i < _buyableBuffBases.Count; i++)
            {
                if (_buyableBuffBases[i] == null)
                    continue;
                Destroy(_buyableBuffBases[i].gameObject);
            }

            _buyableBuffBases?.Clear();
        }

        for (int i = 0; i < 4; i++)
        {
            BuyableBuffBase BuyableBuffBase = Instantiate(_buyableBuffBase, _parent);
            BuyableBuffBase.SetItem(_items[Random.Range(0, _items.Length)]);
            _buyableBuffBases.Add(BuyableBuffBase);
        }
    }
}