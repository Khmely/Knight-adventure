using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class Volume : MonoBehaviour
{
    AudioMixer audioMixer;

    public void SetVolume(float vol)
    {
        audioMixer.SetFloat("Volume", vol);
    }
}
