using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "VisualValues", menuName = "ScriptableObjects/VisualValues")]
public class VisualValues : ScriptableObject
{
    public float RingVfxBaseRadius;// = 1.25f;
    public float ringVfxDecrement;// = .15f;

    public float MarqueeMoveSpeed; // = 1
    public float MarqueeRespawn; // = 5
    public float MarqueeDespawn; // = -5

    public float ButtonDefaultPos;//
    public float ButtonPressedPos;//
    public float ButtonReleaseSpeed;//
}
