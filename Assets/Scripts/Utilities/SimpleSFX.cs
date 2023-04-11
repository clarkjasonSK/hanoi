using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SFX", menuName = "ScriptableObjects/SimpleSFX")]
public class SimpleSFX : SFXEvent
{
	 public AudioClip AudioClip;

	[Range(0, 1.0f)] public float Volume=1f;

	[Range(0, 3f)] public float Pitch=1f;

	public override void PlaySFX(AudioSource src)
	{
		if (AudioClip is null || src.isPlaying)
        {
			return;
		}
		src.volume = Volume;
		src.pitch = Pitch;
		src.PlayOneShot(AudioClip);
	}
}
