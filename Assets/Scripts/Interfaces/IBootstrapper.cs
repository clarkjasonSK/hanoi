using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBootstrapper
{
    void LoadSingletonsAndDependencies();
}
