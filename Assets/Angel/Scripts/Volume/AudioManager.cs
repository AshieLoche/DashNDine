using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private VolumeData volumeData;
    [SerializeField] private List<AudioSource> sfxAudioSources;
    [SerializeField] private AudioSource bgmAudioSource;
    [Header("Audio Clips")]
    [SerializeField] private AudioClip normalBGM, ambushAudio, deathAudio;

    private void Update()
    {
        foreach (AudioSource source in sfxAudioSources)
        {
            source.volume = volumeData.sfxVolume / 100;
        }
        bgmAudioSource.volume = volumeData.bgmVolume / 100;
    }
    public void playAmbush()
    {
        bgmAudioSource.clip = ambushAudio;
        bgmAudioSource.Play();
    }
    public void playDeath()
    {
        bgmAudioSource.clip = deathAudio;
        bgmAudioSource.Play();
    }
    public void playNorm()
    {
        bgmAudioSource.clip = normalBGM;
        bgmAudioSource.Play();
    }
}
