using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract  class SFXEvent : ScriptableObject
{
    public abstract void PlaySFX(AudioSource src);
}
