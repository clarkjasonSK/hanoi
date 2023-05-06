using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoleData : MonoBehaviour, IResettable
{
    [SerializeField] private bool _is_hovering;
    public bool IsHovering
    {
        get { return _is_hovering; }
        set { _is_hovering = value; }
    }

    private Stack<Ring> _ring_stack = new Stack<Ring>();

    public Ring TopRing
    {
        get { return _ring_stack.Peek(); }
    }
    public int StackCount
    {
        get { return _ring_stack.Count; }
    }

    public void AddRing(Ring ring)
    {
        _ring_stack.Push(ring);
    }
    public Ring RemoveTopRing()
    {
        return _ring_stack.Pop();
    }
    public void DepleteStack()
    {
        _ring_stack.Clear();
    }

    public void Reset()
    {
        _is_hovering = false;
        DepleteStack();
    }

}
