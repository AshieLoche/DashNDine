using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUIManager : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject startPromptPanel;

    [Header("Buttons")]
    [SerializeField] private Button PlayBtn;
    [SerializeField] private Button SettingsBtn, QuitBtn, SettingsCloseBtn;

    [Header("Audio")]
    [SerializeField] private VolumeData volumeData;
    [SerializeField] private AudioSource sfxAudioSource, bgmAudioSource;
    [SerializeField] private AudioClip sfxAudioClip, playBtnAudioClip, bgmAudioClip;
    [SerializeField] private Slider sfxVolumeSlider, bgmVolumeSlider;
    [SerializeField] private TextMeshProUGUI sfxVolumeLevelTxt, bgmVolumeTxt;

    [Header("Player")]
    [SerializeField] private PlayerData playerData;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainMenuPanel.SetActive(false);
        PlayBtn.onClick.AddListener(StartGame);
        SettingsBtn.onClick.AddListener(Settings);
        SettingsCloseBtn.onClick.AddListener(CloseSettings);
        QuitBtn.onClick.AddListener(Quit);

        sfxVolumeSlider.onValueChanged.AddListener(UpdateSFXVolumeLevel);
        bgmVolumeSlider.onValueChanged.AddListener(UpdateBGMVolumeLevel);
        
        playerData.currentHP = playerData.maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        if (InputSystem.actions.FindAction("Submit").WasPerformedThisFrame())
        {
            sfxAudioSource.clip = playBtnAudioClip;
            sfxAudioSource.Play();
            bgmAudioSource.clip = bgmAudioClip;
            bgmAudioSource.Play();
            mainMenuPanel.SetActive(true);
            startPromptPanel.SetActive(false);
        }
        UpdateVolumeIndicator();
    }
    void UpdateSFXVolumeLevel(float value)
    {
        volumeData.sfxVolume = value;
        sfxAudioSource.volume =volumeData.sfxVolume/100;
        sfxVolumeLevelTxt.text = value.ToString();
    }
    void UpdateBGMVolumeLevel(float value)
    {
        volumeData.bgmVolume = value;
        bgmAudioSource.volume = volumeData.bgmVolume/100;
        bgmVolumeTxt.text = value.ToString();
    }
    void UpdateVolumeIndicator()
    {
        sfxVolumeSlider.value = volumeData.sfxVolume;
        bgmVolumeSlider.value = volumeData.bgmVolume;
    }
    void StartGame()
    {
        sfxAudioSource.clip = playBtnAudioClip;
        sfxAudioSource.Play();
        SceneManager.LoadScene("Game");
    }
    void Settings()
    {
        sfxAudioSource.clip = sfxAudioClip;
        sfxAudioSource.Play();
        //mainMenuPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }
    void CloseSettings()
    {
        sfxAudioSource.clip = sfxAudioClip;
        sfxAudioSource.Play();
        //mainMenuPanel.SetActive(true);
        settingsPanel.SetActive(false);
    }
    void Quit()
    {
        sfxAudioSource.clip = sfxAudioClip;
        sfxAudioSource.Play();
        Application.Quit();
    }
}
