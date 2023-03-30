using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoleData : MonoBehaviour
{
    [SerializeField] [Range(1, 3)] private int _pole_position;
    public int PolePosition
    {
        get { return _pole_position; }
        set { _pole_position = value; }
    }

    [SerializeField] private bool _is_hovering;
    public bool IsHovering
    {
        get { return _is_hovering; }
        set { _is_hovering = value; }
    }

    [SerializeField] private Stack<Ring> _ring_stack;

    public Ring GetRemoveTopRing
    {
        get { return _ring_stack.Pop(); }
    }
    public Ring GetTopRing
    {
        get { return _ring_stack.Peek(); }
    }
    public int GetStackCount
    {
        get { return _ring_stack.Count; }
    }

    public void AddRing(Ring ring)
    {
        _ring_stack.Push(ring);
    }
    public void DepleteStack()
    {
        _ring_stack.Clear();
    }


}
