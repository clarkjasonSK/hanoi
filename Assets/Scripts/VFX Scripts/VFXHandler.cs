using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXHandler : Singleton<VFXHandler>, IEventObserver
{
    [SerializeField] private VFXRefs _vfx_refs;

    [SerializeField] private VisualValues _vis_vals;



    #region Cache Variables
    private Ring ringHitKey;
    //private Vector3 ringHitPos;
    private GameObject tempGameObj;
    #endregion

    public override void Initialize()
    {
        if (_vfx_refs is null)
            _vfx_refs = GetComponent<VFXRefs>();

        if (_vis_vals is null)
            _vis_vals = AssetManager.Instance.VisualValues;

        _vfx_refs.RingHitPool.startPooling();

        AddEventObservers();

        isDone = true;
    }
    public void AddEventObservers()
    {
        EventBroadcaster.Instance.AddObserver(EventKeys.RING_HIT, OnRingHit);
    }

    public void ReleaseVFX(GameObject vfx)
    {
        _vfx_refs.RingHitPool.GameObjectPool.Release(vfx);
    }

    private void setRingHitKeyRefs(EventParameters param)
    {
        ringHitKey = param.GetParameter<Ring>(EventParamKeys.RING, null);
        //ringHitPos = param.GetParameter<Vector3>(EventParamKeys.RING_HIT_VFX, Vector3.zero);
    }
    #region Event Broadcaster Notifications

    public void OnRingHit(EventParameters param)
    {
        setRingHitKeyRefs(param);

        tempGameObj = _vfx_refs.RingHitPool.GameObjectPool.Get();
        tempGameObj.transform.position = ringHitKey.transform.position;

        ParticleSystem.ShapeModule tempShape = tempGameObj.GetComponent<ParticleSystem>().shape;

        tempShape.radius = _vis_vals.RingVfxBaseRadius - ((ringHitKey.RingSize-1)* _vis_vals.ringVfxDecrement);

        tempGameObj.GetComponent<ParticleSystem>().Play();

    }

    #endregion


}
