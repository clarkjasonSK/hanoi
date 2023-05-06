using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "VisualValues", menuName = "ScriptableObjects/VisualValues")]
public class VisualValues : ScriptableObject
{
    public float RingVfxBaseRadius;// = 1.25f;
    public float ringVfxDecrement;// = .15f;

    public float MarqueeMoveSpeed;// = .15f;
    public float MarqueeRespawn;// = .15f;
    public float MarqueeDespawn;// = .15f;
}
