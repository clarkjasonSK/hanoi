using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoleRefs : MonoBehaviour
{
    public RingHandler RingHandler;
    public PoleLifetime PoleLifetime;
    public ObjectPooling ObjectPooling;
    public PolePosition[] PositionArray;

    private void Start()
    {
        if (RingHandler is null)
        {
            RingHandler = GameObject.FindGameObjectWithTag(TagNames.RING_PARENT).GetComponent<RingHandler>();
        }
    }
}
