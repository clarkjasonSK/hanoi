using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXHandler : Singleton<VFXHandler>, IEventObserver
{
    [SerializeField] private VFXRefs _vfx_refs;

    #region Cache Variables
    //private Ring ringKey;
    private ParticleSystem ringKeyPartSys;
    private GameObject tempVfx;
    #endregion
    public override void Initialize()
    {
        if (_vfx_refs is null)
            _vfx_refs = GetComponent<VFXRefs>();

        AddEventObservers();

        isDone = true;
    }
    public void AddEventObservers()
    {
        EventBroadcaster.Instance.AddObserver(EventKeys.RING_HIT, OnRingHit);
    }

    public void ReleaseVFX(GameObject vfx)
    {
        _vfx_refs.VFXObjectPool.GameObjectPool.Release(vfx);
    }

    private void setRingHitKeyRefs(EventParameters param)
    {
        //ringKey = param.GetParameter<Ring>(EventParamKeys.RING, null);
        ringKeyPartSys = param.GetParameter<ParticleSystem>(EventParamKeys.RING_HIT_VFX, null);
    }
    #region Event Broadcaster Notifications

    public void OnRingHit(EventParameters param)
    {
        setRingHitKeyRefs(param);

        tempVfx = _vfx_refs.VFXObjectPool.GameObjectPool.Get();
        //CopyComponent<ParticleSystem>(ringKeyPartSys, tempVfx);
        tempVfx.transform.position = ringKeyPartSys.gameObject.transform.position;
        tempVfx.GetComponent<ParticleSystem>().Play();

    }

    #endregion


    // from this: https://answers.unity.com/questions/458207/copy-a-component-at-runtime.html
    private T CopyComponent<T>(T original, GameObject destination) where T : Component
    {
        System.Type type = original.GetType();
        var dst = destination.GetComponent(type) as T;
        if (!dst) dst = destination.AddComponent(type) as T;
        var fields = type.GetFields();
        foreach (var field in fields)
        {
            if (field.IsStatic) continue;
            field.SetValue(dst, field.GetValue(original));
        }
        var props = type.GetProperties();
        foreach (var prop in props)
        {
            if (!prop.CanWrite || !prop.CanWrite || prop.Name == "name") continue;
            prop.SetValue(dst, prop.GetValue(original, null), null);
        }
        return dst as T;
    }

}
