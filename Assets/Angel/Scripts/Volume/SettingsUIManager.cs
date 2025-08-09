using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsUIManager : MonoBehaviour
{
    [Header("Panel")]
    [SerializeField] private GameObject backGround;
    [SerializeField] private GameObject settingsPanel;

    [Header("Buttons")]
    [SerializeField] private Button SettingsBtn;
    [SerializeField] private Button SettingsCloseBtn;
    [SerializeField] private Button BackToMain;
    [Header("Slides")]
    [SerializeField] private Slider sfxVolumeSlider;
    [SerializeField] private Slider bgmVolumeSlider;
    [Header("Volume Data")]
    [SerializeField] private VolumeData volumeData;
    [Header("OnClick SFX")]
    [SerializeField] private AudioClip onClickClip;
    [Header("Audio Source")]
    [SerializeField] private AudioSource sfxAudioSource;
    [Header("Audio Lables")]
    [SerializeField] private TextMeshProUGUI sfxVolumeLevelTxt;
    [SerializeField] private TextMeshProUGUI bgmVolumeTxt;

    void Start()
    {
        SettingsBtn.onClick.AddListener(Settings);
        SettingsCloseBtn.onClick.AddListener(CloseSettings);
        BackToMain.onClick.AddListener(MainMenu);

        sfxVolumeSlider.onValueChanged.AddListener(UpdateSFXVolumeLevel);
        bgmVolumeSlider.onValueChanged.AddListener(UpdateBGMVolumeLevel);

    }
    private void Update()
    {
        UpdateVolumeIndicator();
    }
    void UpdateSFXVolumeLevel(float value)
    {
        volumeData.sfxVolume = value;
        sfxVolumeLevelTxt.text = value.ToString();
    }
    void UpdateBGMVolumeLevel(float value)
    {
        volumeData.bgmVolume = value;
        bgmVolumeTxt.text = value.ToString();
    }
    void UpdateVolumeIndicator()
    {
        sfxVolumeSlider.value = volumeData.sfxVolume;
        bgmVolumeSlider.value = volumeData.bgmVolume;
    }
    void Settings()
    {
        sfxAudioSource.clip = onClickClip;
        sfxAudioSource.Play();
        backGround.SetActive(true);
        settingsPanel.SetActive(true);
    }
    void CloseSettings()
    {
        sfxAudioSource.clip = onClickClip;
        sfxAudioSource.Play();
        backGround.SetActive(false);
        settingsPanel.SetActive(false);
    }
    void MainMenu()
    {
        sfxAudioSource.clip = onClickClip;
        sfxAudioSource.Play();
        SceneManager.LoadScene("Main Menu");
    }
}
