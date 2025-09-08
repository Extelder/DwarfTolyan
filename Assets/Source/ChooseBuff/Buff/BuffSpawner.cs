using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class BuffSpawner : MonoBehaviour
{
    [SerializeField] private Transform _parent;

    [SerializeField] private Item[] _items;

    [SerializeField] private BuyableBuffBase _buyableBuffBase;

    private BuyableBuffBase[] _buyableBuffBases = new BuyableBuffBase[4];

    private void OnEnable()
    {
        Reroll();
    }

    public void Reroll()
    {
        for (int i = 0; i < _buyableBuffBases.Length; i++)
        {
            if (_buyableBuffBases[i] == null)
            {
                continue;
            }

            if (!_buyableBuffBases[i].Locked)
            {
                Destroy(_buyableBuffBases[i].gameObject);
                _buyableBuffBases[i] = null;
            }
        }

        for (int i = 0; i < _buyableBuffBases.Length; i++)
        {

            if (_buyableBuffBases[i])
                if (_buyableBuffBases[i].gameObject != null)
                    continue;

            BuyableBuffBase BuffBase = Instantiate(_buyableBuffBase, _parent);
            BuffBase.SetItem(_items[Random.Range(0, _items.Length)]);
            _buyableBuffBases[i] = BuffBase;
        }
    }
}