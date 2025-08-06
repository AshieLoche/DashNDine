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

    private void Start()
    {
        HideInteractionPanels();
        acceptBtn.onClick.AddListener(HideInteractionPanels);
        noBtn.onClick.AddListener(HideInteractionPanels);
    }

    // Update is called once per frame
    void Update()
    {
        if (InputSystem.actions.FindAction("Click").WasPerformedThisFrame())
        {
            StopCoroutine(TypeSentence(dialogueTxtHolder));
            dialogueTxt.text = dialogueTxtHolder;
            skipPrompt.SetActive(false);
            ShowQuestPanel();
        }
    }

    public void OpenDialogue(NPCManager npc, QuestData questData)
    {
        dialoguePanel.SetActive(true);
        npcName.text = npc.GetName();
        npcImage.GetComponent<Image>().sprite = npc.GetImage();
        dialogueTxtHolder = questData.taskDialogue;
        questObjectiveTxt.text = questData.taskDescription;
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
    public void ShowQuestPanel()
    {
        questPromptPanel.SetActive(true);
    }
    public void HideInteractionPanels()
    {
        dialoguePanel.SetActive(false);
        questPromptPanel.SetActive(false);
    }
}
