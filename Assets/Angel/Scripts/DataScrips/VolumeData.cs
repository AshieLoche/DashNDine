using NUnit.Framework;
using UnityEngine;
using RangeAttribute = UnityEngine.RangeAttribute;

[CreateAssetMenu(menuName = "Scriptables/Volume Data")]

public class VolumeData : ScriptableObject
{
    [Range(0,100)]public float sfxVolume = 100, bgmVolume = 100;
}
