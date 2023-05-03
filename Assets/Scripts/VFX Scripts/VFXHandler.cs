using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXHandler : Singleton<VFXHandler>, IEventObserver
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

        isDone = true;
    }
    public void AddEventObservers()
    {
        EventBroadcaster.Instance.AddObserver(EventKeys.RING_HIT, OnRingHit);
    }

    public void ReleaseVFX(GameObject vfx)
    {
        _vfx_refs.VFXLifetime.ReturnRingVFX(vfx.GetComponent<SimpleVFX>());
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

    #endregion


}
