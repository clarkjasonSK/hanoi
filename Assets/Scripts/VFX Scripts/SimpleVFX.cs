using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleVFX : Poolable
{
    private EventParameters _vfx_param;

    private void Start()
    {
        _vfx_param = new EventParameters();
        _vfx_param.AddParameter(EventParamKeys.VFX_PARTICLE, this);
    }
    void OnParticleSystemStopped()
    {
        if(poolOrigin is null)
        {
            this.gameObject.SetActive(false);
            return;
        }

        EventBroadcaster.Instance.PostEvent(EventKeys.VFX_STOP, _vfx_param);
       // VFXHandler.Instance.ReleaseVFX(this.gameObject);
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
