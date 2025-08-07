using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUIManager : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject settingsPanel;

    [Header("Buttons")]
    [SerializeField] private Button PlayBtn;
    [SerializeField] private Button SettingsBtn, QuitBtn, SettingsCloseBtn;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip sfxAudioClip, bgmAudioClip;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainMenuPanel.SetActive(false);
        PlayBtn.onClick.AddListener(StartGame);
        SettingsBtn.onClick.AddListener(Settings);
        SettingsCloseBtn.onClick.AddListener(CloseSettings);
        QuitBtn.onClick.AddListener(Quit);
    }

    // Update is called once per frame
    void Update()
    {
        if (InputSystem.actions.FindAction("Submit").WasPerformedThisFrame())
        {
            mainMenuPanel.SetActive(true);
        }
    }

    void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
    void Settings()
    {
        //mainMenuPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }
    void CloseSettings()
    {
        //mainMenuPanel.SetActive(true);
        settingsPanel.SetActive(false);
    }
    void Quit()
    {
        Application.Quit();
    }
}
