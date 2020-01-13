using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeChange : MonoBehaviour
{
    public AudioMixer audioMixer;

    public void SetVolume(float vol)
    {
        audioMixer.SetFloat("Volume", vol);
    }
    public void SetEffectsVolume(float vol)
    {
        audioMixer.SetFloat("Effects", vol);
    }
}
