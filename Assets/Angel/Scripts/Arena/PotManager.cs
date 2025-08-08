using UnityEngine;

public class PotManager : MonoBehaviour
{
    [SerializeField] private int maxHP = 5;
    [SerializeField] private int currentHP;
    [SerializeField] private GameObject spawnVFX, dmgVFX, destroyVFX;
    [SerializeField] private AudioClip spawnSFX, dmgSFX, destroyedSFX;
    [SerializeField] private PotDefenseManager defenseManager;
    private AudioSource audioSrc;
    private bool isBeingDestroyed = false;

    private void Awake()
    {
        audioSrc = GetComponent<AudioSource>();
        if (audioSrc == null)
            gameObject.AddComponent<AudioSource>();
    }
    private void Update()
    {
        if (currentHP == 0 && !isBeingDestroyed)
        {
            isBeingDestroyed = true;
            PlayAudioClip(destroyedSFX);
            if (!destroyVFX.activeSelf)destroyVFX.SetActive(true);
        }
        if (isBeingDestroyed && !audioSrc.isPlaying)
        {
            defenseManager.PotDestroyed();
            gameObject.SetActive(false);
        } 
    }
    private void OnEnable()
    {
        isBeingDestroyed = false;
        currentHP = maxHP;
        spawnVFX.SetActive(true);
        audioSrc.clip = spawnSFX;
        audioSrc.Play();
    }
    public void DamagePot()
    {
        PlayAudioClip(dmgSFX);
        currentHP --;
    }
    void PlayAudioClip (AudioClip sfx)
    {
        audioSrc.clip = sfx;
        audioSrc.Play();
    }

}
