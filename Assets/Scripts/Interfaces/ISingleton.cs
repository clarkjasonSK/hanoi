using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISingleton
{
    public bool IsDoneInitializing { get; }
    void Initialize();
}
