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
    #endregion

    public void Initialize()
    {
        _pole_queue = new Queue<Pole>();
        AddEventObservers();

        isDone = true;
    }
    public void AddEventObservers()
    {
        EventBroadcaster.Instance.AddObserver(EventKeys.GAME_START, OnGameStart);

        EventBroadcaster.Instance.AddObserver(EventKeys.RING_ADDPOLE, OnRingAdd);
        EventBroadcaster.Instance.AddObserver(EventKeys.POLE_PRESS, OnPolePress);
        EventBroadcaster.Instance.AddObserver(EventKeys.POLE_HOVER, OnPoleHover);

        EventBroadcaster.Instance.AddObserver(EventKeys.ASSETS_RESET, OnAssetsReset);
    }
    
    private void setPoleRef(EventParameters param)
    {
        poleRef = param.GetParameter<Pole>(EventParamKeys.SELECTED_POLE,null);
    }
    private void addRing()
    {
        poleRef.AddRingToPole(_origin_pole.RemoveTopRing());

        if(poleRef.GetRingCount()==GameManager.Instance.RingAmount )
        {
            EventBroadcaster.Instance.PostEvent(EventKeys.POLE_FULL, null);
        }
    }
    private void dropRing()
    {
        RingHandler.Instance.DropRing(poleRef.transform.localPosition);
        EventBroadcaster.Instance.PostEvent(EventKeys.RING_MOVE, null);
    }

    #region Event Broadcaster Notifications
    public void OnGameStart(EventParameters param = null)
    {
        
        for(int i=0; i<3; i++)
        {
            poleRef = _pole_util.PoleLifetime.GetPole();
            _pole_queue.Enqueue(poleRef);

            // from end pole to beggining pole
            poleRef.PolePosition = _pole_util.PositionArray[_pole_util.PositionArray.Length - 1 - i]; 
            _pole_util.PositionArray[_pole_util.PositionArray.Length - 1 - i].PoleRef = poleRef;

            poleRef.transform.localPosition = _pole_util.PositionArray[_pole_util.PositionArray.Length-3-i].GetLocation(); // set at two locations back
            poleRef.MoveToPolePosition();
        }

    }

    public void OnRingAdd(EventParameters param)
    {
        _pole_util.PositionArray[2].PoleRef.AddRingToPole(param.GetParameter<Ring>(EventParamKeys.RING, null));
    }

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
            addRing();
            dropRing();
            return;
        }


        // if destination pole top ring is smaller than floating ring (smaller == higher number)
        if (poleRef.BorrowTopRing().RingSize > RingHandler.Instance.FloatingRingSize) 
            return;

        // if destination pole's top ring is bigger than floating ring (i.e. if equal, skip this step)
        if (!(poleRef.BorrowTopRing().RingSize == RingHandler.Instance.FloatingRingSize))
        {
            addRing();
        }

        // if equal, just drop 
        dropRing();

    }
    public void OnPoleHover(EventParameters param)
    {
        if (!RingHandler.Instance.HasFloatingRing)
            return;
        
        setPoleRef(param);

        RingHandler.Instance.MoveFloatingRing(poleRef.transform.localPosition);


    }
    public void OnAssetsReset(EventParameters param)
    {
        for(int i=2; i<5; i++)
        {
            _pole_util.PositionArray[i].PoleRef.ResetPole();
        }
        /*
        _pole_util.PoleFirst.DepletePole();
        _pole_util.PoleSecond.DepletePole();
        _pole_util.PoleThird.DepletePole();*/

    }

    #endregion
}
