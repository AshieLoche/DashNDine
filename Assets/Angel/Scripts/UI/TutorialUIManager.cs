using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class TutorialUIManager : MonoBehaviour
{
    [Header("Tutorial UI")]
    [SerializeField] private GameObject tutorialPanel;
    [SerializeField] private VideoPlayer player;
    [SerializeField] private TextMeshProUGUI tutorialPrompt;
    [SerializeField] private GameObject background; 
    [Header("Buttons")]
    [SerializeField] private Button closeBtn;
    [SerializeField] private Button next;
    [SerializeField] private Button previous;
    [Header("Video List")]
    [SerializeField] private List<VideoClip> clipList = new List<VideoClip>();
    [SerializeField] private List<string> instructionList = new List<string>();
    private int selectedIndex = 0;

    private void Start()
    {
        closeBtn.onClick.AddListener(CloseTutorial);
        next.onClick.AddListener(NextTutorial);
        previous.onClick.AddListener(PreviousTutorial);

        player.clip = clipList[0];
        tutorialPrompt.text = instructionList[0];
    }
    private void Update()
    {
        if (selectedIndex == 0)
            previous.gameObject.SetActive(false);
        else if(selectedIndex>0) previous.gameObject.SetActive(true);

        if (selectedIndex == instructionList.Count - 1)
            next.gameObject.SetActive(false);
        else if (selectedIndex < instructionList.Count - 1)
            next.gameObject.SetActive(true);
    }
    void NextTutorial()
    {
        selectedIndex = Mathf.Clamp(selectedIndex + 1, 0,instructionList.Count - 1);
        UpdateTutorial();
    }
    void PreviousTutorial()
    {
        selectedIndex = Mathf.Clamp(selectedIndex - 1, 0, instructionList.Count - 1);
        UpdateTutorial();
    }
    void UpdateTutorial()
    {
        player.clip = clipList[selectedIndex];
        tutorialPrompt.text = instructionList[selectedIndex];
    }
    void CloseTutorial()
    {
        background.SetActive(false);
        tutorialPanel.SetActive(false);
    }
}
