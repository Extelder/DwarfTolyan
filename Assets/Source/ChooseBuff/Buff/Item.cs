using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Buff/Item")]
public class Item : ScriptableObject
{
    [field: SerializeField] public int Cost { get; protected set; }
    [field: SerializeField] public Sprite Icon { get; protected set; }
    [field: SerializeField] public string Desc { get; protected set; }

    public virtual void Buy()
    {
        ItemSpawner.Instance.SpawnItem(this);
        Debug.LogError(name + "bought");
    }
}