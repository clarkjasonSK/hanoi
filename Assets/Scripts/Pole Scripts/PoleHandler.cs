using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoleHandler : Singleton<PoleHandler>, ISingleton, IEventObserver
{
    #region ISingleton Variables
    private bool isDone = false;
    public bool IsDoneInitializing
    {
        get { return isDone; }
    }
    #endregion

    [SerializeField] private PoleUtility _pole_util;
    public PoleUtility PoleUtility
    {
        set { _pole_util = value; }
    }
    [SerializeField] private Pole _origin_pole;

    private Queue<Pole> _pole_queue;


    #region Cache Parameter Refs
    Pole poleRef;
    Ring ringRef;
    #endregion

    public void Initialize()
    {
        /*
        _pole_queue = new Queue<Pole>();
        _pole_queue.Enqueue(_pole_util.PoleLifetime.GetPole());*/

        for(int i=0; i< GameManager.Instance.RingAmount; i++)
        {
            _pole_util.PoleFirst.AddRingToPole(RingHandler.Instance.GetRingAt(i));
        }
        AddEventObservers();

        isDone = true;
    }
    public void AddEventObservers()
    {
        EventBroadcaster.Instance.AddObserver(EventKeys.POLE_PRESS, OnPolePress);
        EventBroadcaster.Instance.AddObserver(EventKeys.POLE_HOVER, OnPoleHover);
    }
    
    private void setPoleRef(EventParameters param)
    {
        poleRef = param.GetParameter<Pole>(EventParamKeys.SELECTED_POLE,null);
    }
    private void setRingRef(EventParameters param)
    {
        ringRef = param.GetParameter<Ring>(EventParamKeys.SELECTED_RING, null);
    }
    private void setRefs(EventParameters param)
    {
        setPoleRef(param);
        setRingRef(param);
    }

    #region Event Broadcaster Notifications
    public void OnPolePress(EventParameters param = null)
    {
        setPoleRef(param);

        if (!RingHandler.Instance.HasFloatingRing) // if no floating ring, try to make top ring float
        {
            if (poleRef.GetRingCount() == 0)
                return;

            _origin_pole = poleRef;
            RingHandler.Instance.FloatRing(_origin_pole.BorrowTopRing());

            return;
        }

        // else if has floating ring, try to drop it

        // destination pole has no rings
        if (poleRef.GetRingCount() == 0)
        {
            poleRef.AddRingToPole(_origin_pole.RemoveTopRing());
            RingHandler.Instance.DropRing(poleRef.transform.localPosition);
            return;
        }


        // if destination pole top ring is smaller than floating ring
        if (poleRef.BorrowTopRing().RingSize < RingHandler.Instance.FloatingRingSize) 
            return;


        // if destination pole's top ring is bigger than floating ring (i.e. if equal, skip this step)
        if(!(poleRef.BorrowTopRing().RingSize == RingHandler.Instance.FloatingRingSize))
        {
            poleRef.AddRingToPole(_origin_pole.RemoveTopRing());
        }

        // if equal, just drop 
        RingHandler.Instance.DropRing(poleRef.transform.localPosition);
        
    }
    public void OnPoleHover(EventParameters param)
    {
        if (!RingHandler.Instance.HasFloatingRing)
            return;
        
        setPoleRef(param);

        RingHandler.Instance.MoveFloatingRing(poleRef.transform.localPosition);


    }
    #endregion
}
