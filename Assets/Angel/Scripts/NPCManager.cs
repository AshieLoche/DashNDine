using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    [Header("NPC Information")]
    [SerializeField] private NPCData npcData;
    [SerializeField] private int npcID;
    [SerializeField] private string npcName;
    [SerializeField] private Sprite image;
    [SerializeField] private int completedQuests;

    [Header("Quest Information")]
    [SerializeField] private List<QuestData> quests;
    [SerializeField] private bool isInProgress, isSuccessful;
    public bool IsInProgress => isInProgress;
    public bool IsSuccessful => isSuccessful;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        npcID = npcData.npcID;
        npcName = npcData.npcName;
        image = npcData.npcImage;
        completedQuests = npcData.completedQuests;

        if(npcID != 0) quests = GameManager.Instance.questManager.GetNPCQuests(npcID);
    }

    public QuestData GiveQuest()
    {
        return quests[completedQuests];
    }
    public string GetName()
    {
        return npcName;
    }
    public Sprite GetImage()
    {
        return image;
    }
    public void AcceptQuest()
    {
        Debug.Log("Quest has been accepted");
        isInProgress = true;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Entered");
        GameManager.Instance.dialogueManager.OpenDialogue(this, quests[completedQuests]);
    }
}
