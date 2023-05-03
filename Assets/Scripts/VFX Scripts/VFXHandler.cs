using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXHandler : Handler
{
    [SerializeField] private VFXRefs _vfx_refs;


    [SerializeField] private VisualValues _vis_vals;

    #region Cache Variables
    #endregion

    public override void Initialize()
    {
        if (_vfx_refs is null)
            _vfx_refs = GetComponent<VFXRefs>();

        if (_vis_vals is null)
            _vis_vals = AssetManager.Instance.VisualValues;

        _vfx_refs.VFXLifetime.StartPooling();

        AddEventObservers();
    }
    public override void AddEventObservers()
    {
        EventBroadcaster.Instance.AddObserver(EventKeys.RING_HIT, OnRingHit);
        EventBroadcaster.Instance.AddObserver(EventKeys.VFX_STOP, OnVFXStop);
    }

    private void setRingVFX(GameObject newVFX, Ring ring)
    {
        newVFX.transform.position = ring.transform.position;
        setVFXRadius(newVFX.GetComponent<ParticleSystem>().shape, ring.RingSize);

        newVFX.GetComponent<ParticleSystem>().Play();
    }
    private void setVFXRadius(ParticleSystem.ShapeModule vfxShape, int targetSize)
    {
        vfxShape.radius = _vis_vals.RingVfxBaseRadius - ((targetSize - 1) * _vis_vals.ringVfxDecrement);
    }

    #region Event Broadcaster Notifications

    public void OnRingHit(EventParameters param)
    {

        setRingVFX(_vfx_refs.VFXLifetime.GetRingVFX(GameManager.Instance.GoalPoleWhole), param.GetParameter<Ring>(EventParamKeys.RING, null));

    }
    public void OnVFXStop(EventParameters param)
    {
        _vfx_refs.VFXLifetime.ReturnRingVFX(param.GetParameter<SimpleVFX>(EventParamKeys.VFX_PARTICLE, null));
    }

    #endregion


}
