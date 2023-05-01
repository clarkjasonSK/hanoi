using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleVFX : Poolable
{
    void OnParticleSystemStopped()
    {
        VFXHandler.Instance.ReleaseVFX(this.gameObject);
    }

    #region Poolable
    public override void OnInstantiate()
    {
    }

    public override void OnActivate()
    {

    }

    public override void OnDeactivate()
    {

    }

    #endregion
}
