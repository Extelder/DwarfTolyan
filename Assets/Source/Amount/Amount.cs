using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Amount : MonoBehaviour
{
    [SerializeField] protected float costByMove;
    [SerializeField] protected float delayAfterSpendToEarn = 0.1f;
    [Range(0, 1)][SerializeField] protected float capacity;
    [SerializeField] protected float earnSpeed;

    protected float current;

    protected bool earn = true;

    private void OnEnable()
    {
        current = capacity;
    }

}
