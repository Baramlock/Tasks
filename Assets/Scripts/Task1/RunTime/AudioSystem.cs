using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioSystem : MonoBehaviour
{
    public List<AudioSystemData> Date;
    public AudioSource AudioSource;

    public void Init() => AudioSource = GetComponent<AudioSource>();

    public void Play(AudioSystemData data)
    {
        AudioSource.clip = data.AudioClip;
        AudioSource.Play();
    }

    public void Pause() => AudioSource.Stop();

    public void UpgradeData(AudioSystemData data)
    {
        AudioSource.pitch = data.Pitch;
        AudioSource.volume = data.Volume;
    }
}