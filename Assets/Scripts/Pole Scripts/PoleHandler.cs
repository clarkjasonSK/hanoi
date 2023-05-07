using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoleHandler : Handler
{

    [SerializeField] private PoleRefs _pole_refs;

    private Queue<Pole> _pole_queue;
    private Pole _origin_pole;

    #region Cache Parameter Refs
    private Pole poleRef;
    #endregion

    public override void Initialize()
    {
        _pole_refs = GetComponent<PoleRefs>();
        _pole_refs.PoleLifetime.StartPool();

        _pole_queue = new Queue<Pole>();
        AddEventObservers();
    }
    public override void AddEventObservers()
    {
        EventBroadcaster.Instance.AddObserver(EventKeys.ASSETS_INIT, OnPolesInit);
        EventBroadcaster.Instance.AddObserver(EventKeys.ASSETS_DISABLE, OnAssetsDisable);
        EventBroadcaster.Instance.AddObserver(EventKeys.ASSETS_RESET, OnAssetsReset);

        EventBroadcaster.Instance.AddObserver(EventKeys.POLE_SPAWN, OnPoleSpawn);
        EventBroadcaster.Instance.AddObserver(EventKeys.POLE_DESPAWN, OnPoleDespawn);
        EventBroadcaster.Instance.AddObserver(EventKeys.POLE_ADJUST, OnPoleAdjust);

        EventBroadcaster.Instance.AddObserver(EventKeys.POS_ENTER, OnPosEnter);
        EventBroadcaster.Instance.AddObserver(EventKeys.POS_PRESS, OnPosPress);

        EventBroadcaster.Instance.AddObserver(EventKeys.POLE_ADD_RING, OnPoleAddRing);


    }
    
    private void setPoleRef(EventParameters param)
    {
        poleRef = param.GetParameter<Pole>(EventParamKeys.POLE,null);
    }
    private void addRing()
    {
        poleRef.AddRingToPole(_origin_pole.RemoveTopRing());

        if(poleRef.GetPolePosition() == PoleDictionary.END_POS && poleRef.GetRingCount()==GameManager.Instance.RingAmount )
        {
            EventBroadcaster.Instance.PostEvent(EventKeys.POLE_END_FULL, null);
        }
    }
    private void dropRing()
    {
        _pole_refs.RingHandler.DropRing(poleRef.transform.localPosition);
        EventBroadcaster.Instance.PostEvent(EventKeys.RING_DROPPED, null);
    }

    private Pole createPole()
    {
        poleRef = _pole_refs.PoleLifetime.GetPole();
        poleRef.ResetPole(GameManager.Instance.RingAmount);
        _pole_queue.Enqueue(poleRef);
        return poleRef;
    }
    private void setPole(Pole pole, int posIndex, int locIndex)
    {
        pole.PolePosition = _pole_refs.PositionArray[posIndex];
        _pole_refs.PositionArray[posIndex].PoleRef = pole;
        _pole_refs.PositionArray[posIndex].Initialize();

        if (locIndex != PoleDictionary.SPAWN_POS)
        {
            pole.transform.localPosition = _pole_refs.PositionArray[locIndex].GetLocation();
        }

        pole.MoveToPolePosition();
    }

    #region Event Broadcaster Notifications
    public void OnPolesInit(EventParameters param = null)
    {
        
        for(int i = _pole_refs.PositionArray.Length - 1; 0 < i; i--)
        {
            // from end pole to beggining pole, set at two locations back
            setPole(createPole(), i, i-1);
        }

    }

    public void OnPoleSpawn(EventParameters param)
    {
        setPole(createPole(), 0, 0);
    }

    public void OnPosEnter(EventParameters param)
    {
        if (!_pole_refs.RingHandler.HasFloatingRing)
            return;

        setPoleRef(param);
        _pole_refs.RingHandler.MoveFloatingRing(poleRef.transform.localPosition);
    }
    public void OnPosPress(EventParameters param = null)
    {
        setPoleRef(param);

        if (!_pole_refs.RingHandler.HasFloatingRing) // if no floating ring, try to make top ring float
        {
            if (poleRef.GetRingCount() == 0)
                return;

            _origin_pole = poleRef;
            _pole_refs.RingHandler.FloatRing(_origin_pole.BorrowTopRing());

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
        if (poleRef.BorrowTopRing().RingSize > _pole_refs.RingHandler.FloatingRingSize) 
            return;

        // if destination pole's top ring is bigger than floating ring (i.e. if equal, skip this step)
        if (!(poleRef.BorrowTopRing().RingSize == _pole_refs.RingHandler.FloatingRingSize))
        {
            addRing();
        }

        // if equal, just drop 
        dropRing();

    }
    public void OnPoleAddRing(EventParameters param)
    {
        _pole_refs.PositionArray[PoleDictionary.SPAWN_INDEX].PoleRef.AddRingToPole(param.GetParameter<Ring>(EventParamKeys.RING, null));
    }


    public void OnPoleDespawn(EventParameters param)
    {
        setPoleRef(param);

        _pole_refs.PoleLifetime.ReturnPole(_pole_queue.Dequeue());
    }
    public void OnPoleAdjust(EventParameters param)
    {
        
        for (int i = _pole_refs.PositionArray.Length - 1; 0 < i; i--)
        { // move leading poles forward
            setPole(_pole_refs.PositionArray[i-1].PoleRef, i, -1);
        }

    }
    public void OnAssetsDisable(EventParameters param = null)
    {
        _pole_refs.PositionArray[_pole_refs.PositionArray.Length - 1].TogglePosColliders(false);
    }

    public void OnAssetsReset(EventParameters param = null)
    {
        _pole_refs.PositionArray[_pole_refs.PositionArray.Length - 1].TogglePosColliders(true);
        for (int i=2; i<5; i++)
        {
            _pole_refs.PositionArray[i].PoleRef.ResetPole(GameManager.Instance.RingAmount);
        }


    }

    #endregion
}
