using NUnit.Framework.Interfaces;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [Header("NPC")]
    [SerializeField] private TextMeshProUGUI npcName;
    [SerializeField] private GameObject npcImage;

    [Header("Dialogue Box")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueTxt;
    [SerializeField] private float typingSpeed = 0.05f;
    [SerializeField] private GameObject skipPrompt;
    private string dialogueTxtHolder;

    [Header("Quest Prompt")]
    [SerializeField] private GameObject questPromptPanel;
    [SerializeField] private TextMeshProUGUI questObjectiveTxt;
    [SerializeField] private Button acceptBtn, noBtn;
    private bool isAcceptingQuest = false;

    [Header("Dialogue Collection")]
    [SerializeField] string csvPath;
    [SerializeField] List<DialogueData> dialogueDataSet;
    [SerializeField] DialogueCSVLoader csvLoader;

    private void Start()
    {
        HideInteractionPanels();
        acceptBtn.onClick.AddListener(HideInteractionPanels);
        noBtn.onClick.AddListener(HideInteractionPanels);
        dialogueDataSet = csvLoader.LoadCSV(csvPath);
    }

    // Update is called once per frame
    void Update()
    {
        if (InputSystem.actions.FindAction("Click").WasPerformedThisFrame() && !questPromptPanel.activeSelf && isAcceptingQuest)
        {
            StopCoroutine(TypeSentence(dialogueTxtHolder));
            dialogueTxt.text = dialogueTxtHolder;
            skipPrompt.SetActive(false);
            ShowQuestPanel();
        }
    }

    public void OpenDialogue(NPCManager npc, QuestData questData)
    {
        isAcceptingQuest = true;
        dialoguePanel.SetActive(true);
        npcName.text = npc.GetName();
        npcImage.GetComponent<Image>().sprite = npc.GetImage();
        dialogueTxtHolder = GetDialogueTxt(questData.questID, npc.IsSuccessful,npc.IsInProgress);
        questObjectiveTxt.text = questData.Description;
        acceptBtn.onClick.AddListener(npc.AcceptQuest);
        StartCoroutine(TypeSentence(dialogueTxtHolder));
    }

    private IEnumerator TypeSentence(string sentence)
    {
        dialogueTxt.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            dialogueTxt.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        ShowQuestPanel();
    }

    private string GetDialogueTxt(int taskID, bool isSuccessful, bool isInProgress)
    {
        foreach(var dialogue in dialogueDataSet)
        {
            if(dialogue.taskID == taskID)
            {
                if (!isInProgress && !isSuccessful)
                    return dialogue.acceptDialogue;
                else if (isInProgress && !isSuccessful)
                    return dialogue.unfinishedDialogue;
                else if (!isInProgress && isSuccessful)
                    return dialogue.finishDialogue;
                else if (!isInProgress && !isSuccessful)
                    return dialogue.failedDialogue;
                else
                    return null;
            }     
        }
        return null;
    }

    public void ShowQuestPanel()
    {
        questPromptPanel.SetActive(true);
    }

    public void HideInteractionPanels()
    {
        isAcceptingQuest = false;
        dialoguePanel.SetActive(false);
        questPromptPanel.SetActive(false);
    }
}
