using DashNDine.EnumSystem;
using DashNDine.ScriptableObjectSystem;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestUI : MonoBehaviour
{
    [Header("For Debugging Only")]
    [SerializeField] QuestSO quest;
    [SerializeField] bool start;
    [SerializeField] bool isQuestComplete;

    [Header("Quests")]
    [SerializeField] private IngredientStackListSO ingredientList;
    [SerializeField] private List<IngredientSO> ingredients;
    [SerializeField] private Canvas questCanvas;
    private QuestSO acceptedQuests;

    [Header("Panels")]
    [SerializeField] private GameObject questPanel;

    [Header("Buttons")]
    [SerializeField] private Button toggleBtn;
    [SerializeField] private Vector3 questPanelMovedPos;

    [Header("Texts")]
    [SerializeField] private TextMeshProUGUI npcName, questDescription, questRequirement;
    private List<string> questItems;

    [SerializeField] GameObject hideSpot;

    private Vector3 initialPos;
    private Vector2 initSize;
    private bool isMinimized = false;

    private void Start()
    {
        initialPos = toggleBtn.GetComponent<RectTransform>().position;
        toggleBtn.onClick.AddListener(Resize);
    }

    private void Update()
    {
        if(acceptedQuests != null) 
            isQuestComplete = acceptedQuests.QuestStatus == QuestStatus.Success || acceptedQuests.QuestStatus == QuestStatus.Failure ? true : false;
        if (start)
        {
           StartCoroutine(StartQuest(quest));
        }

        if (isQuestComplete)
        {
            MovePosition(initialPos, questCanvas, initSize, Vector3.one);
            toggleBtn.gameObject.SetActive(false);
            Close();
            isQuestComplete = false;
        }
    }

    public IEnumerator StartQuest(QuestSO quest)
    {
        questPanel.SetActive(true);
        toggleBtn.gameObject.SetActive(true);
        acceptedQuests = quest;
        ingredientList = quest.GetQuestObjectiveList();
        ingredients = ingredientList.GetIngredientSOList();

        npcName.text = acceptedQuests.Name;
        questDescription.text = acceptedQuests.Description;
        questRequirement.text = acceptedQuests.Name;
        start = false;
        yield return null;
    }

    public void Close()
    {
        questPanel.SetActive(false);
        toggleBtn.gameObject.SetActive(false);
    }

    public void Resize() 
    {
        Debug.Log("Maximize has been pressed.");
        if (!isMinimized)
        {
            isMinimized = true;
            initSize = toggleBtn.gameObject.GetComponent<RectTransform>().sizeDelta;
            Vector2 scale = initSize / 2f;
            MovePosition(hideSpot.transform.position, questCanvas, scale, new Vector3(0.3f,0.3f,0.3f));
        }
        else if (isMinimized) 
        {
            isMinimized = false;
            MovePosition(initialPos, questCanvas, initSize, Vector3.one);
        }

    }
    void MovePosition(Vector3 hidespotPos, Canvas qCanvas, Vector2 scale, Vector3 panelScale)
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(hidespotPos);

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            qCanvas.transform as RectTransform,
            screenPos,
            qCanvas.worldCamera,
            out Vector2 localPos
        );
        questPanel.GetComponent<RectTransform>().localPosition = localPos;
        toggleBtn.GetComponent<RectTransform>().localPosition = localPos;
        toggleBtn.GetComponent<RectTransform>().localScale = panelScale;
        questPanel.GetComponent<RectTransform>().localScale = panelScale;
    }
}
