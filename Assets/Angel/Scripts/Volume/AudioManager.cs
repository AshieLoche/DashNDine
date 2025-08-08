using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private VolumeData volumeData;
    [SerializeField] private List<AudioSource> sfxAudioSources;
    [SerializeField] private AudioSource bgmAudioSource;
    [Header("Audio Clips")]
    [SerializeField] private AudioClip bgmAudioClip;

    private void Update()
    {
        foreach (AudioSource source in sfxAudioSources)
        {
            source.volume = volumeData.sfxVolume / 100;
        }
        bgmAudioSource.volume = volumeData.bgmVolume / 100;
    }
}
