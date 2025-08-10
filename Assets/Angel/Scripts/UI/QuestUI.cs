using DashNDine.ScriptableObjectSystem;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestUI : MonoBehaviour
{
    [Header("Quests")]
    [SerializeField] private QuestListSO questList;
    private List<QuestSO> acceptedQuests;

    [Header("Panels")]
    [SerializeField] private GameObject questPanel;

    [Header("Buttons")]
    [SerializeField] private Button questToggleBtn;
    [SerializeField] private Button quest1Btn, quest2Btn, quest3Btn;
    [SerializeField] private Vector3 questPanelMovedPos;

    [Header("Texts")]
    [SerializeField] private TextMeshProUGUI activeQuest1, activeQuest2, activeQuest3, questDescription, questRequirement;

    [Header("Image")]
    [SerializeField] private Image characterPortrait;

    private Vector3 initialPos;

    private void Start()
    {
        initialPos = questPanel.GetComponent<RectTransform>().position;
        acceptedQuests = new List<QuestSO>();
    }
    public void Open()
    {
        questPanel.GetComponent<RectTransform>().position = questPanelMovedPos;

        quest1Btn.onClick.AddListener(ShowQuest1);
        quest2Btn.onClick.AddListener(ShowQuest2);
        quest3Btn.onClick.AddListener(ShowQuest3);

    }
    public void Close()
    {
        questPanel.GetComponent<RectTransform>().position = questPanelMovedPos;
    }
    public void ShowQuest1()
    {

    }
    public void ShowQuest2()
    {

    }
    public void ShowQuest3()
    {

    }
}
