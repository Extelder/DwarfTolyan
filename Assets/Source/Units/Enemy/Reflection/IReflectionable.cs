using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IReflectionable
{
    public EnemyHealth Health { get; set; }
    
    public void TakeReflection();
}
